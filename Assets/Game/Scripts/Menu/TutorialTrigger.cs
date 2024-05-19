using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI tutorialText;
    [SerializeField] public bool isPrimerTuto;
    private bool Retardo = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isPrimerTuto && Retardo)
            {
                StartCoroutine(EsperarYContinuar(collision));
            }
            else
            {
                ProcesarEntrada(collision);
            }
        }
    }

    private IEnumerator EsperarYContinuar(Collider2D collision)
    {
        yield return new WaitForSeconds(4); // Espera 3 segundos.
        Retardo = false; // Después de esperar, establece Retardo a false para que no se aplique de nuevo.
        ProcesarEntrada(collision); // Continúa con el procesamiento.
    }

    private void ProcesarEntrada(Collider2D collision)
    {
        string message = GetMessageBasedOnTriggerName(gameObject.name); // Obtiene el mensaje.
        tutorialText.text = message; // Establece el mensaje.
        CancelInvoke("HideText");
        Invoke("HideText", 30f); // Oculta el mensaje después de 30 segundos.
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideText();
        }
    }

    private void HideText()
    {
        tutorialText.text = ""; // Limpia el texto
    }

    // Esta función decide qué mensaje mostrar basado en el nombre del trigger
    private string GetMessageBasedOnTriggerName(string triggerName)
    {
        switch (triggerName)
        {
            case "msg1":
                return "Bienvenido a The Seed´s Odyssey, prueba a moverte con el joystick!";
            case "msg2":
                return "Prueba a Saltar pulsando el boton 'A'! ";
            case "msg3":
                return "Puedes saltar en las paredes para impulsarte a la otra pared!";
            case "msg4":
                return "Cuidado con este enemigo! Pulsando el boton 'B' te deslizaras, que puede serte muy util!";
            case "msg5":
                return "Una Valiosa gema! si la consigues mientras deslizas, podras deslizar de nuevo!";
            default:
                return "Mensaje no definido para este área.";
        }
    }
}