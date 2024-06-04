using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    public PlayerData Data;

    #region JOYSTICK
    public FixedJoystick joystick;

    #endregion

    #region COMPONENTS
    public Rigidbody2D RB { get; private set; }
    //Script to handle all player animations, all references can be safely removed if you're importing into your own project.
    #endregion

    #region STATE PARAMETERS
    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public static bool IsDashing { get; private set; }
    public bool IsSliding { get; private set; }
    public static bool isDying = false;
    public static bool isRespawning = false;
    public static bool isWindUpActive = false;

    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnGroundTime { get; private set; }
    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }

    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;

    //Wall Jump
    private float _wallJumpStartTime;
    private int _lastWallJumpDir;

    //Dash
    public int _dashesLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;

    #endregion

    public static bool isEmpowered = false;

    #region INPUT PARAMETERS
    public Vector2 _moveInput;

    public float LastPressedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    #endregion

    #region CHECK PARAMETERS
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    [SerializeField] private float windForce = 0;
    [SerializeField] private float windUpForce = 0;
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private Animator animator;

    public Light2D luzPersonaje;

    public bool forceMoveRight = false;

    public bool ral;


    public SpriteRenderer Jugador;// Ahora este campo contendr� la referencia al SpriteRenderer del jugador
    public Color dashColor = Color.red; // Color during dash
    public Color normalColor = Color.white; // Normal color
    public Color empoweredColor = Color.yellow; // Color when empowered

    private Vector2 checkPointPosition;

    private bool isFallingAnimation = false;

    private float fallAnimationCooldown = 0.2f; // Time in seconds to wait before the "Fall" animation can be triggered again
    private float fallAnimationTimer = 0;

    private bool wasOnGround;

    private void Awake()
    {

        RB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isWindUpActive = false;
        // Aqu� puedes asignar la referencia al SpriteRenderer del jugador
        Jugador = GetComponent<SpriteRenderer>();

        SetGravityScale(Data.gravityScale);
        IsFacingRight = true;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        animator.SetBool("isDying", isDying);



        HandleInput();


        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;

        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;
        #endregion

        #region INPUT HANDLER

        bool isOnGround = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);

        if (isOnGround && !wasOnGround && !PlayerMovement.isRespawning && !PlayerMovement.isDying) // If the player is on the ground, was not on the ground in the previous frame, and is not respawning
        {
            isFallingAnimation = false;

            // Check if the "Apareciendo" animation is playing
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Apareciendo"))
            {
                // If it's not, play the "Quieto" animation immediately
                animator.Play("Quieto");
            }
        }

        wasOnGround = isOnGround;

        fallAnimationTimer -= Time.deltaTime;

        if (RB.velocity.y < 0 && !isFallingAnimation && !Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !PlayerMovement.isRespawning && !animator.GetCurrentAnimatorStateInfo(0).IsName("Apareciendo"))
        {
            animator.SetTrigger("Fall");
            isFallingAnimation = true;
            fallAnimationTimer = fallAnimationCooldown; // Reset the timer
        }



        // Si se están utilizando los controles del teclado, establece isUsingKeyboard a true
        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        //{
        //    isUsingKeyboard = true;
        //}
        //// Si se está utilizando el joystick, establece isUsingKeyboard a false
        //else if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        //{
        //    isUsingKeyboard = false;
        //}
        if (!isDying)
        {

            //_moveInput.x = forceMoveRight ? 1 : Input.GetAxisRaw("Horizontal");
            //_moveInput.y = Input.GetAxisRaw("Vertical");

            //if (isUsingKeyboard)
            //{
            //    _moveInput.x = forceMoveRight ? 1 : Input.GetAxisRaw("Horizontal");
            //    _moveInput.y = Input.GetAxisRaw("Vertical");
            //}
            //else
            //{
            //    // Si la entrada del joystick es mayor que un pequeño umbral, se considera que es máxima

            // Add a small deadzone
            if (joystick.Direction.magnitude < 0.1f)
            {
                _moveInput = Vector2.zero;
            }
            else
            {
                // Normalize to 8 directions
                float angle = Mathf.Atan2(joystick.Vertical, joystick.Horizontal);
                angle = Mathf.Round(angle / (Mathf.PI / 4)) * (Mathf.PI / 4);
                _moveInput = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                _moveInput.x = forceMoveRight ? 1 : _moveInput.x;
            }

            //}


        }

        bool isRunning = Mathf.Abs(_moveInput.x) > 0;
        animator.SetBool("isRunning", isRunning);

        //if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.J)) && !isWindUpActive)
        //{
        //    OnJumpInput();
        //}

        //if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.C) || Input.GetKeyUp(KeyCode.J)) && !isWindUpActive)
        //{
        //    OnJumpUpInput();
        //}

        //if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.K)) && !isWindUpActive)
        //{
        //    OnDashInput();
        //}
        #endregion

        #region COLLISION CHECKS
        if (!IsDashing && !IsJumping)
        {
            //Ground Check
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer)) //checks if set box overlaps with ground
            {
                if (!isEmpowered)
                {
                    Jugador.color = normalColor;
                }
                LastOnGroundTime = Data.coyoteTime; //if so sets the lastGrounded to coyoteTime
                isFallingAnimation = false;
            }
            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
                LastOnWallRightTime = Data.coyoteTime;

            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
                LastOnWallLeftTime = Data.coyoteTime;

            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
        }
        #endregion

        #region JUMP CHECKS
        if (IsJumping && RB.velocity.y < 0)
        {
            IsJumping = false;

            _isJumpFalling = true;
        }

        if (IsWallJumping && Time.time - _wallJumpStartTime > Data.wallJumpTime)
        {
            IsWallJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
            _isJumpCut = false;

            _isJumpFalling = false;
        }

        if (!IsDashing)
        {
            //Jump
            if (CanJump() && LastPressedJumpTime > 0)
            {
                IsJumping = true;
                IsWallJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;
                Jump();
            }
            //WALL JUMP
            else if (CanWallJump() && LastPressedJumpTime > 0)
            {
                IsWallJumping = true;
                IsJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;

                _wallJumpStartTime = Time.time;
                _lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

                WallJump(_lastWallJumpDir);
            }
        }
        #endregion

        #region DASH CHECKS
        if (CanDash() && LastPressedDashTime > 0)
        {
            //Freeze game for split second. Adds juiciness and a bit of forgiveness over directional input
            Sleep(Data.dashSleepTime);

            //If not direction pressed, dash forward
            if (_moveInput != Vector2.zero)
                _lastDashDir = _moveInput;
            else
                _lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;



            IsDashing = true;
            IsJumping = false;
            IsWallJumping = false;
            _isJumpCut = false;

            StartCoroutine(nameof(StartDash), _lastDashDir);
        }
        #endregion

        #region SLIDE CHECKS
        if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
            IsSliding = true;
        else
            IsSliding = false;
        #endregion

        #region GRAVITY
        if (!_isDashAttacking)
        {
            //Higher gravity if we've released the jump input or are falling
            if (IsSliding)
            {
                SetGravityScale(0);
            }
            else if (RB.velocity.y < 0 && _moveInput.y < 0)
            {
                //Much higher gravity if holding down
                SetGravityScale(Data.gravityScale * Data.fastFallGravityMult);
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFastFallSpeed));
            }
            else if (_isJumpCut)
            {
                //Higher gravity if jump button released
                SetGravityScale(Data.gravityScale * Data.jumpCutGravityMult);
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
            }
            else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
            {
                SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult);
            }
            else if (RB.velocity.y < 0)
            {


                //Higher gravity if falling
                SetGravityScale(Data.gravityScale * Data.fallGravityMult);
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Data.maxFallSpeed));
            }
            else
            {
                //Default gravity if standing on a platform or moving upwards
                SetGravityScale(Data.gravityScale);
            }
        }
        else
        {
            //No gravity when dashing (returns to normal once initial dashAttack phase over)
            SetGravityScale(0);
        }
        #endregion  

        if (ral)
        {
            SlowDownPlayer(0.5f);
            Invoke("restablecer", 5.0f);
        }
        if (isWindUpActive)
        {
            // Si el jugador está subiendo y hay una fuerza de viento hacia arriba, cambia a la animación de caída.
            animator.SetBool("isRunning", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Colision con " + collision.tag);

        if (isDying)
        {
            return;
        }

        if (collision.CompareTag("GeM"))
        {
            Debug.Log("Colision con GeM");

            Jugador.color = normalColor;
            if (_dashesLeft == 0)
            {
                _dashesLeft++;
            }
            AudioManager.instance.PlayAudio();
            GameObject parentObject = collision.gameObject.transform.parent.gameObject;
            if (parentObject != null)
            {
                parentObject.SetActive(false);
                StartCoroutine(ReaparecerObjeto(parentObject));
            }

            // Ralentiza el tiempo a la mitad de la velocidad normal
            Time.timeScale = 0.4f;

            StartCoroutine(RestoreTimeAfterDelay(0.4f));

        }

        if (collision.CompareTag("GemBoss"))
        {
            if (_dashesLeft == 0)
            {
                _dashesLeft++;
            }
            AudioManager.instance.PlayAudio();
            isEmpowered = true;
            Jugador.color = empoweredColor;
            GameObject parentObject = collision.gameObject.transform.parent.gameObject;
            Destroy(parentObject);
        }

        if (collision.CompareTag("GemaAmarilla"))
        {
            AudioManager.instance.PlayAudio();
            GameObject parentObject = collision.gameObject.transform.parent.gameObject;
            GemaAmarilla gema = parentObject.GetComponent<GemaAmarilla>();
            int gemaId = gema.gemaId;
            PlayerPrefs.SetInt("GemaAmarilla" + gemaId, 1);
            Destroy(parentObject);
        }






        if (collision.CompareTag("CheckPoint"))
        {
            // Guarda la posición del checkpoint
            checkPointPosition = collision.transform.position;
        }

        if (collision.CompareTag("windActiveCollider"))
        {
            isWindUpActive = true;
            animator.SetTrigger("Fall");
        }
    }

    private IEnumerator RestoreTimeAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
    }
    private IEnumerator DesactivarDespuesDeAudio(AudioSource audioSource, GameObject objetoADesactivar)
    {
        // Espera mientras el audio esté reproduciéndose
        yield return new WaitWhile(() => audioSource.isPlaying);

        // Desactiva el objeto después de que el audio ha terminado de reproducirse
        objetoADesactivar.SetActive(false);
    }

    // M�todo para ralentizar al jugador
    public void SlowDownPlayer(float reductionFactor)
    {
        Vector2 currentVelocity = RB.velocity;
        currentVelocity.x *= reductionFactor;
        RB.velocity = currentVelocity;
        ChangeColor("#5E0C8E");
    }

    // M�todo para cambiar el color del SpriteRenderer
    public void ChangeColor(string hexColor)
    {
        Color colorPersonalizado = HexToColor(hexColor);
        Jugador.color = colorPersonalizado;
    }

    // M�todo para convertir una cadena hexadecimal a un color en Unity
    private Color HexToColor(string hexColor)
    {
        Color color = Color.white;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Error al convertir el color hexadecimal.");
            return Color.white;
        }
    }

    // M�todo para restablecer la velocidad al jugador
    public void restablecer()
    {
        Vector2 currentVelocity = RB.velocity;
        currentVelocity.x *= 1.0f;
        RB.velocity = currentVelocity;
        ral = false;
        ChangeColor("#FFFFFF");
    }


    private IEnumerator ReaparecerObjeto(GameObject objeto)
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(3);

        // Reactiva el objeto
        if (objeto != null && !isEmpowered)
        {
            objeto.SetActive(true);
        }
    }

    private void HandleInput()
    {

        // Actualizar la dirección a la que mira el personaje
        if (_moveInput.x != 0)
        {
            CheckDirectionToFace(_moveInput.x > 0);
        }

        // Actualizar el parámetro de animación "isRunning" basado en la entrada
        bool isRunning = Mathf.Abs(_moveInput.x) > 0;
        animator.SetBool("isRunning", isRunning);
    }

    private void FixedUpdate()
    {
        //40 wind force horizontal

        if (!isWindUpActive)
        {
            RB.AddForce(new Vector2(windForce, 0));
        }
        else
        {
            RB.velocity = new Vector2(RB.velocity.x, windUpForce);
        }




        if (isDying)
        {
            luzPersonaje.enabled = false;
            return;
        }
        else
        {
            luzPersonaje.enabled = true;
        }

        //Handle Run
        if (!IsDashing)
        {
            if (IsWallJumping)
                Run(Data.wallJumpRunLerp);
            else
            {

                Run(1);
            }

        }
        else if (_isDashAttacking)
        {
            Run(Data.dashEndRunLerp);
        }

        //Handle Slide
        if (IsSliding)
            Slide();


    }

    #region INPUT CALLBACKS
    //Methods which whandle input detected in Update()
    public void OnJumpInput()
    {
        if (!isDying)
        {
            if (!CanJump())
            {
                animator.SetTrigger("Jump");
            }
            LastPressedJumpTime = Data.jumpInputBufferTime;
        }
    }

    public void OnJumpUpInput()
    {
        if (!isDying)
        {
            if (CanJumpCut() || CanWallJumpCut())
                _isJumpCut = true;
        }
    }

    public void OnDashInput()
    {
        if (!isDying)
        {
            LastPressedDashTime = Data.dashInputBufferTime;
            if (!isEmpowered)
            {
                Jugador.color = dashColor;
            }
        }
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        RB.gravityScale = scale;
    }

    private void Sleep(float duration)
    {
        //Method used so we don't need to call StartCoroutine everywhere
        //nameof() notation means we don't need to input a string directly.
        //Removes chance of spelling mistakes and will improve error messages if any
        StartCoroutine(nameof(PerformSleep), duration);
    }

    private IEnumerator PerformSleep(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale with be 0 
        Time.timeScale = 1;
    }
    #endregion

    //MOVEMENT METHODS
    #region RUN METHODS
    private void Run(float lerpAmount)
    {


        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.x * Data.runMaxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
        if (Data.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - RB.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        RB.AddForce(movement * Vector2.right, ForceMode2D.Force);

        /*
		 * For those interested here is what AddForce() will do
		 * RB.velocity = new Vector2(RB.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / RB.mass, RB.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/

    }

    private void Turn()
    {
        // Voltear el personaje y actualizar el estado de IsFacingRight
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    #endregion

    #region JUMP METHODS
    private void Jump()
    {
        //Ensures we can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = Data.jumpForce;
        if (RB.velocity.y < 0)
            force -= RB.velocity.y;

        RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion


    }

    private void WallJump(int dir)
    {
        //Ensures we can't call Wall Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;
        LastOnWallLeftTime = 0;

        #region Perform Wall Jump
        Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
        force.x *= dir; //apply force in opposite direction of wall

        if (Mathf.Sign(RB.velocity.x) != Mathf.Sign(force.x))
            force.x -= RB.velocity.x;

        if (RB.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
            force.y -= RB.velocity.y;

        //Unlike in the run we want to use the Impulse mode.
        //The default mode will apply are force instantly ignoring masss
        RB.AddForce(force, ForceMode2D.Impulse);
        #endregion
    }
    #endregion

    #region DASH METHODS
    //Dash Coroutine
    private IEnumerator StartDash(Vector2 dir)
    {
        //Overall this method of dashing aims to mimic Celeste, if you're looking for
        // a more physics-based approach try a method similar to that used in the jump

        LastOnGroundTime = 0;
        LastPressedDashTime = 0;

        float startTime = Time.time;

        _dashesLeft--;
        _isDashAttacking = true;

        SetGravityScale(0);

        //We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
        while (Time.time - startTime <= Data.dashAttackTime)
        {
            RB.velocity = dir.normalized * Data.dashSpeed;
            //Pauses the loop until the next frame, creating something of a Update loop. 
            //This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
            yield return null;
        }

        startTime = Time.time;

        _isDashAttacking = false;

        //Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
        SetGravityScale(Data.gravityScale);
        RB.velocity = Data.dashEndSpeed * dir.normalized;

        while (Time.time - startTime <= Data.dashEndTime)
        {
            yield return null;
        }

        //Dash over
        IsDashing = false;


    }

    //Short period before the player is able to dash again
    private IEnumerator RefillDash(int amount)
    {
        //SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
        _dashRefilling = true;
        yield return new WaitForSeconds(Data.dashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(Data.dashAmount, _dashesLeft + 1);
    }
    #endregion

    #region OTHER MOVEMENT METHODS
    private void Slide()
    {
        //We remove the remaining upwards Impulse to prevent upwards sliding
        if (RB.velocity.y > 0)
        {
            RB.AddForce(-RB.velocity.y * Vector2.up, ForceMode2D.Impulse);
        }

        //Works the same as the Run but only in the y-axis
        //THis seems to work fine, buit maybe you'll find a better way to implement a slide into this system
        float speedDif = Data.slideSpeed - RB.velocity.y;
        float movement = speedDif * Data.slideAccel;
        //So, we clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
        //The force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called. For more info research how force are applied to rigidbodies.
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

        RB.AddForce(movement * Vector2.up);
    }
    #endregion


    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    public bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }

    public bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
             (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
    }

    private bool CanJumpCut()
    {
        return IsJumping && RB.velocity.y > 0;
    }

    private bool CanWallJumpCut()
    {
        return IsWallJumping && RB.velocity.y > 0;
    }

    private bool CanDash()
    {
        if (!IsDashing && _dashesLeft < Data.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
        {
            StartCoroutine(nameof(RefillDash), 1);
        }

        return _dashesLeft > 0;
    }

    public bool CanSlide()
    {
        if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
            return true;
        else
            return false;
    }
    #endregion


    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
        Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo") && !PlayerMovement.isDying && !PlayerMovement.isRespawning)
        {
            isFallingAnimation = false;
            if (animator != null)
            {
                animator.Play("Quieto");
            }
        }
    }

    public void forceRight()
    {
        forceMoveRight = true;
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        if (!isDying)
        {
            _moveInput = forceMoveRight ? new Vector2(1, 0) : moveInput;
        }
    }


    private IEnumerator WaitForAnimation(Animator animator, string animationName)
    {
        // Wait until the current animation has finished
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Then play the next animation
        animator.Play(animationName);
    }

}