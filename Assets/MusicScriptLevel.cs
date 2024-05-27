using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScriptLevel : MonoBehaviour
{
    public AudioClip introClip, loopClip, endClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        if (loopClip != null)
        {
            loopClip.LoadAudioData();
            endClip.LoadAudioData();
        }
        StartCoroutine(PlayLoopWhenIntroStops());
    }

    IEnumerator PlayLoopWhenIntroStops()
    {
        if (introClip != null)
        {
            audioSource.clip = introClip;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
        audioSource.clip = loopClip;
        audioSource.loop = true; // Set loop to true
        audioSource.Play();
    }

    public void EndAudio()
    {
        StartCoroutine(EndAudioSources());
    }

    IEnumerator EndAudioSources()
    {
        audioSource.Stop();
        audioSource.loop = false; // Set loop back to false
        audioSource.clip = endClip;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
