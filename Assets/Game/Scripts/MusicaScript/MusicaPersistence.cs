using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaPersistente : MonoBehaviour
{
    // Declara una variable para almacenar la instancia
    private static MusicaPersistente instancia;

    // Declara una variable para el audio source
    private AudioSource audioSource;

    [SerializeField] private AudioClip musicaMenu;
    [SerializeField] private AudioClip musicaTutorial;
    [SerializeField] private AudioClip musicaEscenaAlex;
    [SerializeField] private AudioClip musicaEscenaAntonio;
    [SerializeField] private AudioClip musicaNivelFinal;
    
    [SerializeField] private float duracionFundido = 2.0f;
    private AudioClip ultimoClipReproducido;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }
        else
        {
     
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ultimoClipReproducido=audioSource.clip;
        SceneManager.sceneLoaded += CambiarMusicaSegunEscena;
    }

    void CambiarMusicaSegunEscena(Scene escena, LoadSceneMode modo)
    {
        StartCoroutine(FundidoSuave(escena));
       
    }

    IEnumerator FundidoSuave(Scene escena)
    {
        float tiempoInicio = Time.time;
        float volumenInicial = audioSource.volume;

       

        audioSource.volume = volumenInicial;

        AudioClip nuevoClip = ObtenerNuevoClip(escena);

        if (nuevoClip != null && nuevoClip != ultimoClipReproducido)
        {
            while (audioSource.volume > 0)
            {
                float tiempoPasado = Time.time - tiempoInicio;
                float porcentaje = Mathf.Clamp01(tiempoPasado / duracionFundido);

                audioSource.volume = Mathf.Lerp(volumenInicial, 0, porcentaje);
                yield return null;
            }
            audioSource.clip = nuevoClip;
            if (audioSource.clip == musicaTutorial || escena.name == "Seleccion Niveles" || escena.name == "Menu" || escena.name == "Creditos")
            {
                audioSource.volume = 0.28f;
            }
            else
            {
                audioSource.volume = 1f;
            }
          
            audioSource.Play();
            ultimoClipReproducido = nuevoClip;
        }
        
    }

    AudioClip ObtenerNuevoClip(Scene escena)
    {
        switch (escena.name)
        {
            case "Menu":
            case "Seleccion Niveles":
                return musicaMenu;
            case "Tutorial":
                return musicaTutorial;
            case "Escena Alex":
                return musicaEscenaAlex;
            case "Escena Antonio":
                return musicaEscenaAntonio;
            case "Nivel Final":
                return musicaNivelFinal;
            default:
                return null;
        }
    }
}
