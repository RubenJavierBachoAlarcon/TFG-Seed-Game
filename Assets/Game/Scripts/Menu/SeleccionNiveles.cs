using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeleccionNiveles : MonoBehaviour
{

    [SerializeField] public Button btnNivel1;
    [SerializeField] public Button btnNivel2;
    [SerializeField] public Button btnNivel3;
    [SerializeField] public Button btnNivel4;
    [SerializeField] public Button btnNivel5;
    [SerializeField] public Button btnNivel6;
    [SerializeField] public Button btnNivel7;

    private MusicScript musicScript;

    void Start()
    {
        musicScript = FindObjectOfType<MusicScript>();
        if (!musicScript.as1.isPlaying)
        {
            musicScript.as1.volume = 1f;
            musicScript.as1.Play();
        }

        if (PlayerPrefs.GetInt("Nivel 1 - Cuevas") == 1)
        {
            btnNivel2.interactable = true;
            btnNivel1.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 2 - Cuevas") == 1)
        {
            btnNivel3.interactable = true;
            btnNivel2.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 3 - Cuevas") == 1)
        {
            btnNivel4.interactable = true;
            btnNivel3.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 1 - Bosque") == 1)
        {
            btnNivel5.interactable = true;
            btnNivel4.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 2 - Bosque") == 1)
        {
            btnNivel6.interactable = true;
            btnNivel5.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 1 - Montaña") == 1)
        {
            btnNivel7.interactable = true;
            btnNivel6.GetComponent<Image>().color = Color.green;
        }
        if (PlayerPrefs.GetInt("Nivel 2 - Montaña - Completed") == 1)
        {
            btnNivel7.GetComponent<Image>().color = Color.green;
        }
    }


    public void Nivel1()
    {
        FadeManager.Instance.FadeToScene("Nivel 1 - Cuevas");
        musicScript.StartFade();
    }

    public void Nivel2()
    {
        FadeManager.Instance.FadeToScene("Nivel 2 - Cuevas");
        musicScript.StartFade();
    }

    public void Nivel3()
    {
        FadeManager.Instance.FadeToScene("Nivel 3 - Cuevas");
        musicScript.StartFade();
    }

    public void Nivel4()
    {
        FadeManager.Instance.FadeToScene("Nivel 1 - Bosque");
        musicScript.StartFade();
    }

    public void Nivel5()
    {
        FadeManager.Instance.FadeToScene("Nivel 2 - Bosque");
        musicScript.StartFade();
    }

    public void Nivel6()
    {
        FadeManager.Instance.FadeToScene("Nivel 1 - Montaña");
        musicScript.StartFade();
    }

    public void Nivel7()
    {
        FadeManager.Instance.FadeToScene("Nivel 2 - Montaña");
        musicScript.StartFade();
    }

    public void Menu()
    {
        FadeManager.Instance.FadeToScene("Menu");
    }
}
