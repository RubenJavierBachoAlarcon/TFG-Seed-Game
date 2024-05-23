using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI tutorialText;
    [SerializeField] public float fadeDuration = 1f; // Duración del fade in y fade out
    public PlayerMovement playerMovement;
    private float movementThreshold = 0.1f;
    private bool actionCompleted = false;
    private float actionTimer = 0f;
    private float actionDuration = 2f; // Duración de la acción en segundos
    private string[] strings = new string[] { "Movimiento", "Salto", "Disparo", "Recarga", "Recoger", "Cambio de arma" };

    public void startText()
    {
        tutorialText.text = strings[0];
        StartCoroutine(FadeTextIn());
    }

    private void Update()
    {
        if (tutorialText.text == strings[0])
        {
            CheckMovementCondition();
        }
        else if (tutorialText.text == strings[1])
        {
            CheckJumpCondition();
        }
    }

    private void CheckMovementCondition()
    {
        if (!actionCompleted && playerMovement.RB.velocity.magnitude > movementThreshold)
        {
            actionTimer += Time.deltaTime;
            if (actionTimer >= actionDuration)
            {
                actionCompleted = true;
                StartCoroutine(FadeTextOut());
            }
        }
        else
        {
            actionTimer = 0f; // Reset the timer if the player stops moving
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
        tutorialText.text = "";
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
}
