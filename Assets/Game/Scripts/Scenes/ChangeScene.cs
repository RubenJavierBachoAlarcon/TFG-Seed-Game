using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene Instance { get; private set; }
    [SerializeField] public GameObject gameManager;
    private PauseMenu pauseMenu;
    [SerializeField] public GameObject FinalMenuPanel;
    [SerializeField] public GameObject panelNegro;
    public string nivelActual;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private TextMeshProUGUI scoreText; // Si estás usando TextMesh Pro
    private int finalScore;

    void Start()
    {
        pauseMenu = gameManager.GetComponent<PauseMenu>(); // Agrega esta línea
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (nivelActual != "Nivel 1 - Montaña")
            {
                playerMovement.forceRight();
            }
            PlayerPrefs.SetInt(nivelActual, 1);
            pauseMenu.pauseTimer(); // Cambia gameManager a pauseMenu
            StartCoroutine(ActivatePanel());

        }
    }
    IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(2);
        panelNegro.SetActive(true);
        StartCoroutine(ShowFinalMenuPanel());
    }

    IEnumerator ShowFinalMenuPanel()
    {
        FinalMenuPanel.SetActive(true);
        StartCoroutine(UpdateScore());
        FinalMenuPanel.transform.localScale = Vector3.zero;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2; // 2 es la velocidad de la animación
            FinalMenuPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer);
            yield return null;
        }
    }

    IEnumerator UpdateScore()
    {
        float finalTime = pauseMenu.GetGameTime();
        finalScore = (int)(100000 / finalTime); // Aumenta el numerador para obtener una puntuación más alta
        int currentScore = 0;

        SaveScoreToPlayerPrefs();
        if (nivelActual != "Nivel4")
        {
            while (currentScore < finalScore)
            {
                currentScore += (int)(Time.deltaTime * 500); // Aumenta el factor de incremento para que la puntuación se incremente más rápido
                scoreText.text = currentScore.ToString() + "\nPoints"; // Agrega " Points" al final del texto de la puntuación
                yield return null;
            }
            // Asegúrate de que la puntuación final sea exactamente igual a finalScore
            scoreText.text = finalScore.ToString() + "\nPoints"; // Agrega " Points" al final del texto de la puntuación
        }
        else
        {
            FadeManager.Instance.FadeToScene("Cutscene Final");
        }

        //Verify is actual score is greater than the stored score

        //compare nivelActual string with "Nievel1" or "Nivel2" or "Nivel3" or "Nivel4" with equals method


    }

    void SaveScoreToPlayerPrefs()
    {
        if (nivelActual == "Nivel1")
        {
            PlayerPrefs.SetInt("ScoreNivel1", finalScore);
            Debug.Log("ScoreNivel1: " + PlayerPrefs.GetInt("ScoreNivel1"));
        }
        else if (nivelActual == "Nivel2")
        {
            PlayerPrefs.SetInt("ScoreNivel2", finalScore);
            Debug.Log("ScoreNivel2: " + PlayerPrefs.GetInt("ScoreNivel2"));
        }
        else if (nivelActual == "Nivel3")
        {
            PlayerPrefs.SetInt("ScoreNivel3", finalScore);
            Debug.Log("ScoreNivel3: " + PlayerPrefs.GetInt("ScoreNivel3"));
        }
        else if (nivelActual == "Nivel4")
        {
            PlayerPrefs.SetInt("ScoreNivel4", finalScore);
            Debug.Log("ScoreNivel4: " + PlayerPrefs.GetInt("ScoreNivel4"));
        }

        PlayerPrefs.Save();
    }

    public int GetSomeVariable()
    {
        // Retorna la variable que deseas acceder
        return finalScore;
    }
}
