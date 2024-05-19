using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Referencia estática para fácil acceso
    private AudioSource audioSource;    // Referencia al AudioSource

    private void Awake()
    {
        // Implementa el patrón Singleton para asegurar una única instancia
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: mantener vivo entre escenas
        }
        else
        {
            Destroy(gameObject);
        }

        // Inicializa el AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Método para reproducir el audio
    public void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
