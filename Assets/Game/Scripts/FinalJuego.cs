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
        FadeManager.Instance.FadeToScene("Nivel 2 - Montaña");
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

        SaveScoreToPlayerPrefs("Nivel 2 - Montaña");

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

    void SaveScoreToPlayerPrefs(string nivelActual)
    {
        // Get the stored scores
        int storedScore1 = PlayerPrefs.GetInt(nivelActual + " - 1", 0);
        int storedScore2 = PlayerPrefs.GetInt(nivelActual + " - 2", 0);
        int storedScore3 = PlayerPrefs.GetInt(nivelActual + " - 3", 0);

        // If the current score is higher than any of the stored scores, store the current score
        if (finalScore > storedScore1)
        {
            // Shift the old scores down
            PlayerPrefs.SetInt(nivelActual + " - 3", storedScore2);
            PlayerPrefs.SetInt(nivelActual + " - 2", storedScore1);
            PlayerPrefs.SetInt(nivelActual + " - 1", finalScore);
        }
        else if (finalScore > storedScore2)
        {
            PlayerPrefs.SetInt(nivelActual + " - 3", storedScore2);
            PlayerPrefs.SetInt(nivelActual + " - 2", finalScore);
        }
        else if (finalScore > storedScore3)
        {
            PlayerPrefs.SetInt(nivelActual + " - 3", finalScore);
        }

        Debug.Log("Score 1: " + PlayerPrefs.GetInt(nivelActual + " - 1"));
        Debug.Log("Score 2: " + PlayerPrefs.GetInt(nivelActual + " - 2"));
        Debug.Log("Score 3: " + PlayerPrefs.GetInt(nivelActual + " - 3"));

        PlayerPrefs.Save();
    }
}