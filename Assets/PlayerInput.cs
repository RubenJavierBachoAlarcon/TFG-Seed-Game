using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerInput : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Vector2 moveInput;
    public string level;
    private PauseMenu pauseMenu;
    public GameObject pauseButton;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!PlayerMovement.isWindUpActive)
        {
            if (context.performed)
            {
                playerMovement.OnJumpInput();
            }
            if (context.canceled)
            {
                playerMovement.OnJumpUpInput();
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        // Add a small deadzone
        if (moveInput.magnitude < 0.1f)
        {
            moveInput = Vector2.zero;
        }
        else
        {
            // Normalize to 8 directions
            float angle = Mathf.Atan2(moveInput.y, moveInput.x);
            angle = Mathf.Round(angle / (Mathf.PI / 4)) * (Mathf.PI / 4);
            moveInput = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        playerMovement.SetMoveInput(moveInput);
    }




    public void Dash(InputAction.CallbackContext context)
    {
        if (!PlayerMovement.isWindUpActive)
        {
            if (context.performed)
            {
                playerMovement.OnDashInput();
            }
        }
    }

    public void NextLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 1;
            FadeManager.Instance.FadeToScene(level);
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.timeScale == 0)
            {
                pauseMenu.ResumeGame();
            }
            else
            {
                pauseMenu.PauseGame();
            }
        }
    }
}
