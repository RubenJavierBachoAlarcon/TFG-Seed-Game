using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI tutorialText;
    [SerializeField] public float fadeDuration = 1f; // Duración del fade in y fade out
    public PlayerMovement playerMovement;
    private float movementThreshold = 0.1f;
    private bool actionCompleted = false;
    private float actionTimer = 0f;
    public float movementActionDuration = 1f; // Duración de la acción en segundos

    private string[] strings;

    private string[] keyboardAndMouseStrings = new string[]
{
    "Usa WASD para moverte",
    "Presiona Espacio para saltar",
    "Cuando estés contra una pared, usa Espacio para realizar un salto de pared",
    "Presiona Shift para realizar un desplazamiento rápido"
};

    private string[] gamepadStrings = new string[]
    {
    "Usa el joystick izquierdo para moverte",
    "Presiona A para saltar",
    "Cuando estés contra una pared, presiona A para realizar un salto de pared",
    "Presiona B para realizar un desplazamiento rápido"
    };

    private int currentTutorialIndex = 0;

    public void startText()
    {
        tutorialText.text = strings[0];
        StartCoroutine(FadeTextIn());
    }

    private void Awake()
    {
        switch (GetActiveControlScheme())
        {
            case "Keyboard and Mouse":
                strings = keyboardAndMouseStrings;
                break;
            case "Gamepad":
                strings = gamepadStrings;
                break;
            default:
                Debug.LogWarning("Control scheme not recognized, defaulting to keyboard and mouse tutorial strings.");
                strings = keyboardAndMouseStrings;
                break;
        }
    }

    private void Update()
    {
        switch (currentTutorialIndex)
        {
            case 0:
                CheckMovementCondition();
                break;
            case 1:
                CheckJumpCondition();
                break;
            case 2:
                CheckWallJumpCondition();
                break;
            case 3:
                CheckDashCondition();
                break;
        }
    }

    private string GetActiveControlScheme()
    {
        if (Keyboard.current != null && Mouse.current != null)
        {
            return "Keyboard and Mouse";
        }
        else if (Gamepad.current != null)
        {
            return "Gamepad";
        }
        else
        {
            return "Unknown";
        }
    }

    private void CheckMovementCondition()
    {
        if (!actionCompleted && playerMovement.RB.velocity.magnitude > movementThreshold)
        {
            actionTimer += Time.deltaTime;
            if (actionTimer >= movementActionDuration)
            {
                actionCompleted = true;
                StartCoroutine(FadeTextOut());
            }
        }
        else
        {
            actionTimer = 0f;
        }
    }

    private void CheckJumpCondition()
    {
        if (!actionCompleted && playerMovement.IsJumping)
        {
            actionCompleted = true;
            StartCoroutine(FadeTextOut());
        }
    }

    private void CheckWallJumpCondition()
    {
        if (!actionCompleted && playerMovement.IsWallJumping)
        {
            actionCompleted = true;
            StartCoroutine(FadeTextOut());
        }
    }

    private void CheckDashCondition()
    {
        if (!actionCompleted && PlayerMovement.IsDashing)
        {
            actionCompleted = true;
            StartCoroutine(FadeTextOut());
        }
    }

    private IEnumerator FadeTextIn()
    {
        float elapsedTime = 0f;
        Color color = tutorialText.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            tutorialText.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeTextOut()
    {
        float elapsedTime = 0f;
        Color color = tutorialText.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            tutorialText.color = color;
            yield return null;
        }
        actionCompleted = false;
        currentTutorialIndex++;
        if (currentTutorialIndex < strings.Length)
        {
            tutorialText.text = strings[currentTutorialIndex];
            StartCoroutine(FadeTextIn());
        }
        else
        {
            tutorialText.text = "";
        }
    }
}
