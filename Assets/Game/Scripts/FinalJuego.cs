using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalJuego : MonoBehaviour
{
    [SerializeField] public GameObject EndGamePanel;
    [SerializeField] public GameObject panelNegro;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private int finalScore;

    //Start
    void Start()
    {
        EndGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel Final");
    }

    public void MenuGame()
    {
        Time.timeScale = 1;
        FadeManager.Instance.FadeToScene("Seleccion Niveles");
    }

    public void EndGame()
    {
        StartCoroutine(ActivateGameOverPanel());
    }

    IEnumerator ActivateGameOverPanel()
    {
        Debug.Log("ActivateGameOverPanel started"); // Agrega esta línea
        yield return new WaitForSeconds(15);
        StartCoroutine(UpdateFinalScore());
        panelNegro.SetActive(true);
        Time.timeScale = 0;
        EndGamePanel.SetActive(true);
        yield return StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        Debug.Log("ShowGameOverPanel started"); // Agrega esta línea
        EndGamePanel.transform.localScale = Vector3.zero;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.unscaledDeltaTime * 1; // 2 es la velocidad de la animación
            EndGamePanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer);
            yield return null;
        }
    }

    IEnumerator UpdateFinalScore()
    {
        Debug.Log("UpdateFinalScore started"); // Agrega esta línea
        // Suma las puntuaciones de los 4 niveles
        finalScore = PlayerPrefs.GetInt("Nivel 2 - Montaña");
        int currentScore = 0;

        UpdateHighScores(finalScore);

        while (currentScore < finalScore)
        {
            currentScore += (int)(Time.unscaledDeltaTime * 500); // Usa Time.unscaledDeltaTime en lugar de Time.deltaTime
            finalScoreText.text = currentScore.ToString() + "\nPuntos en Total";
            yield return null;
        }
        // Asegúrate de que la puntuación final sea exactamente igual a finalScore
        finalScoreText.text = finalScore.ToString() + "\nPuntos en Total"; // Agrega " Points" al final del texto de la puntuación

        //Debug de todos los playerPrefs y de la suma
        Debug.Log("ScoreNivel1: " + PlayerPrefs.GetInt("ScoreNivel1"));
        Debug.Log("ScoreNivel2: " + PlayerPrefs.GetInt("ScoreNivel2"));
        Debug.Log("ScoreNivel3: " + PlayerPrefs.GetInt("ScoreNivel3"));
        Debug.Log("ScoreNivel4: " + PlayerPrefs.GetInt("ScoreNivel4"));
        Debug.Log("Final Score: " + finalScore);
    }

    public void UpdateHighScores(int newScore)
    {
        // Comprueba si newScore es mayor que HighScore1
        if (newScore > PlayerPrefs.GetInt("HighScore1", 0))
        {
            // Si newScore es mayor que HighScore1, desplaza los valores actuales hacia abajo
            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2", 0));
            PlayerPrefs.SetInt("HighScore2", PlayerPrefs.GetInt("HighScore1", 0));
            PlayerPrefs.SetInt("HighScore1", newScore);
        }
        // Si no, comprueba si newScore es mayor que HighScore2
        else if (newScore > PlayerPrefs.GetInt("HighScore2", 0))
        {
            // Si newScore es mayor que HighScore2, desplaza los valores actuales hacia abajo
            PlayerPrefs.SetInt("HighScore3", PlayerPrefs.GetInt("HighScore2", 0));
            PlayerPrefs.SetInt("HighScore2", newScore);
        }
        // Si no, comprueba si newScore es mayor que HighScore3
        else if (newScore > PlayerPrefs.GetInt("HighScore3", 0))
        {
            // Si newScore es mayor que HighScore3, actualiza HighScore3
            PlayerPrefs.SetInt("HighScore3", newScore);
        }

        // Guarda los cambios en PlayerPrefs
        PlayerPrefs.Save();
    }
}