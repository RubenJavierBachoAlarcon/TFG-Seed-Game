using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logros : MonoBehaviour
{
    public Button logro1;
    public Sprite logro1Imagen;
    public Button logro2;
    public Sprite logro2Imagen;
    public Button logro3;
    public Sprite logro3Imagen;
    public Button logro4;
    public Sprite logro4Imagen;
    public Button logro5;
    public Sprite logro5Imagen;
    public Button logro6;
    public Sprite logro6Imagen;

    public GameObject panelDescripcionLogro;
    public TextMeshProUGUI tituloLogro;
    public TextMeshProUGUI descripcionLogro;


    void Start()
    {
        if (PlayerPrefs.GetInt("Nivel 1 - Cuevas") == 1 && PlayerPrefs.GetInt("Nivel 2 - Cuevas") == 1 && PlayerPrefs.GetInt("Nivel 3 - Cuevas") == 1)
        {
            logro1.GetComponent<Image>().sprite = logro1Imagen;
        }
        if (PlayerPrefs.GetInt("Nivel 1 - Bosque") == 1 && PlayerPrefs.GetInt("Nivel 2 - Bosque") == 1)
        {
            logro2.GetComponent<Image>().sprite = logro2Imagen;
        }
        if (PlayerPrefs.GetInt("Nivel 1 - Montaña") == 1)
        {
            logro3.GetComponent<Image>().sprite = logro3Imagen;
        }
        if (PlayerPrefs.GetInt("Nivel 2 - Montaña") == 1)
        {
            logro4.GetComponent<Image>().sprite = logro4Imagen;
        }


        if (PlayerPrefs.GetInt("GemaAmarilla0") == 1 && PlayerPrefs.GetInt("GemaAmarilla1") == 1
            && PlayerPrefs.GetInt("GemaAmarilla2") == 1 && PlayerPrefs.GetInt("GemaAmarilla3") == 1
            && PlayerPrefs.GetInt("GemaAmarilla4") == 1 && PlayerPrefs.GetInt("GemaAmarilla5") == 1
            && PlayerPrefs.GetInt("GemaAmarilla6") == 1)
        {
            logro5.GetComponent<Image>().sprite = logro5Imagen;
        }

        Debug.Log(PlayerPrefs.GetInt("GemaAmarilla0") + " " + PlayerPrefs.GetInt("GemaAmarilla1") + " " + PlayerPrefs.GetInt("GemaAmarilla2") + " " + PlayerPrefs.GetInt("GemaAmarilla3") + " " + PlayerPrefs.GetInt("GemaAmarilla4") + " " + PlayerPrefs.GetInt("GemaAmarilla5") + " " + PlayerPrefs.GetInt("GemaAmarilla6"));

        if (PlayerPrefs.GetInt("Nivel 1 - Cuevas" + " - 1", 0) + PlayerPrefs.GetInt("Nivel 2 - Cuevas" + " - 1", 0) + PlayerPrefs.GetInt("Nivel 3 - Cuevas" + " - 1", 0)
            + PlayerPrefs.GetInt("Nivel 1 - Bosque" + " - 1", 0) + PlayerPrefs.GetInt("Nivel 2 - Bosque" + " - 1", 0) + PlayerPrefs.GetInt("Nivel 1 - Montaña" + " - 1", 0)
            + PlayerPrefs.GetInt("Nivel 2 - Montaña" + " - 1", 0) >= 1000)
        {
            logro6.GetComponent<Image>().sprite = logro6Imagen;
        }

    }

    public void abrirPanelDescripcion1()
    {
        tituloLogro.text = "Maestro de las Cuevas";
        descripcionLogro.text = "Completar todos los niveles del ambiente de las cuevas";
        panelDescripcionLogro.SetActive(true);
    }

    public void abrirPanelDescripcion2()
    {
        tituloLogro.text = "Maestro del Bosque";
        descripcionLogro.text = "Completar todos los niveles del ambiente del bosque";
        panelDescripcionLogro.SetActive(true);
    }

    public void abrirPanelDescripcion3()
    {
        tituloLogro.text = "Maestro de la Montaña";
        descripcionLogro.text = "Completar todos los niveles del ambiente de la montaña";
        panelDescripcionLogro.SetActive(true);
    }

    public void abrirPanelDescripcion4()
    {
        tituloLogro.text = "Fin del viaje";
        descripcionLogro.text = "Derrotar a ??? en la cima de la montaña";
        panelDescripcionLogro.SetActive(true);
    }

    public void abrirPanelDescripcion5()
    {
        tituloLogro.text = "Coleccionista";
        descripcionLogro.text = "Encontrar todas las gemas amarillas";
        panelDescripcionLogro.SetActive(true);
    }

    public void abrirPanelDescripcion6()
    {
        tituloLogro.text = "Velocista";
        descripcionLogro.text = "Obtener 1000 puntos en todos los niveles";
        panelDescripcionLogro.SetActive(true);
    }

    public void cerrarPanelDescripcion()
    {
        panelDescripcionLogro.SetActive(false);
    }

    public void Volver()
    {
        FadeManager.Instance.FadeToScene("Menu");
    }
}
