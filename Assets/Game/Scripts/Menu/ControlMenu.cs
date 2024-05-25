using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ControlMenu : MonoBehaviour
{

    [SerializeField] public GameObject btnJugar;
    [SerializeField] public GameObject btnCreditos;
    [SerializeField] public GameObject btnSalir;

    [SerializeField] public GameObject PanelNegro;
    [SerializeField] public GameObject PanelHighScore;
    [SerializeField] public GameObject PanelSeleccionNivel;
    [SerializeField] public GameObject btnHighScore;

    [SerializeField] private TextMeshProUGUI highScore1;
    [SerializeField] private TextMeshProUGUI highScore2;
    [SerializeField] private TextMeshProUGUI highScore3;


    
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        //Debug.Log("Se han borrado los datos");
    }

    public void onButtonNivel(string nivel)
    {
        highScore1.text = "1. " + PlayerPrefs.GetInt(nivel + " - 1", 0).ToString() + " Puntos";
        highScore2.text = "2. " + PlayerPrefs.GetInt(nivel + " - 2", 0).ToString() + " Puntos";
        highScore3.text = "3. " + PlayerPrefs.GetInt(nivel + " - 3", 0).ToString() + " Puntos";

        PanelHighScore.SetActive(true);
        PanelSeleccionNivel.SetActive(false);
    }


    public void onButtonJugar()
    {
        FadeManager.Instance.FadeToScene("Seleccion Niveles");
    }

    public void onButtonCreditos()
    {
        FadeManager.Instance.FadeToScene("Creditos");
    }

    public void onButtonSalir()
    {
        Application.Quit();
    }
    public void onButtonVolver()
    {
        FadeManager.Instance.FadeToScene("Menu");
    }
    public void onButtonHighScore()
    {
        highScore1.text = "1. " + PlayerPrefs.GetInt("HighScore1").ToString() + " Puntos";
        highScore2.text = "2. " + PlayerPrefs.GetInt("HighScore2").ToString() + " Puntos";
        highScore3.text = "3. " + PlayerPrefs.GetInt("HighScore3").ToString() + " Puntos";

        btnHighScore.SetActive(false);
        PanelNegro.SetActive(true);
        PanelSeleccionNivel.SetActive(true);

    }
    public void onButtonCerrarHighScore()
    {
        btnHighScore.SetActive(true);
        PanelNegro.SetActive(false);
        PanelHighScore.SetActive(false);
        PanelSeleccionNivel.SetActive(false);
    }


}
