using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalNivel : MonoBehaviour
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
        float finalTime = pauseMenu.GetGameTime();
        finalScore = (int)(10000 / finalTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt(nivelActual, 1);
            pauseMenu.pauseTimer(); // Cambia gameManager a pauseMenu
            playerMovement.forceRight();
            FadeManager.Instance.FadeToScene("Cutscene Final");
        }
    }

    public int GetSomeVariable()
    {
        // Retorna la variable que deseas acceder
        return finalScore;
    }
}
