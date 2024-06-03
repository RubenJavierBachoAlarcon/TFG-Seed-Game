using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public static MusicScript instance = null;
    public AudioMixer audioMixer;

    public AudioSource as1, as2;
    public float fadeTime = 1f;

    public Button muteButton;
    public Sprite unmuteSprite;
    public Sprite muteSprite;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        UpdateButtonReference();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateButtonReference();
    }

    void UpdateButtonReference()
    {
        GameObject muteButtonObject = GameObject.Find("BtnMute");
        if (muteButtonObject != null)
        {
            muteButton = muteButtonObject.GetComponent<Button>();
            if (muteButton != null)
            {
                muteButton.onClick.AddListener(toggleMute);
                bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
                muteButton.image.sprite = isMuted ? muteSprite : unmuteSprite;
                audioMixer.SetFloat("MasterVolume", isMuted ? -80 : 0);
            }
        }
    }

    public void StartFade()
    {
        StartCoroutine(FadeAudioSources());
    }

    IEnumerator FadeAudioSources()
    {
        as2.Play();
        float startVolume = as1.volume;

        while (as1.volume > 0)
        {
            as1.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        as1.Stop();
    }

    public void toggleMute()
    {
        if (muteButton != null)
        {
            bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
            isMuted = !isMuted;
            PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
            muteButton.image.sprite = isMuted ? muteSprite : unmuteSprite;
            audioMixer.SetFloat("MasterVolume", isMuted ? -80 : 0);
        }
    }
}
