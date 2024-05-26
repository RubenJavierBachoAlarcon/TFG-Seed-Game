using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource as1, as2;
    public float fadeTime = 1f;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartFade()
    {
        StartCoroutine(FadeAudioSources());
    }

    IEnumerator FadeAudioSources()
    {
        as2.Play();
        float startVolume = as1.volume;

        // Fade out as1
        while (as1.volume > 0)
        {
            as1.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        as1.Stop();
    }
}