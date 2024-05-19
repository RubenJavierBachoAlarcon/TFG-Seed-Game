using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private Image blackOverlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Encuentra la imagen negra en la nueva escena
        blackOverlay = GameObject.Find("FondoNegro").GetComponent<Image>();
        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        // Inicia el fundido de negro a la nueva escena
        for (float t = 1; t >= 0; t -= Time.deltaTime)
        {
            if (blackOverlay != null)
            {
                blackOverlay.color = new Color(0, 0, 0, t);
            }
            yield return null;
        }

        // Desactiva el componente de imagen
        if (blackOverlay != null)
        {
            blackOverlay.enabled = false;
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));

    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Activa el componente de imagen
        if (blackOverlay != null)
        {
            blackOverlay.enabled = true;

            // Inicia el fundido a negro
            for (float t = 0; t <= 1; t += Time.deltaTime)
            {
                if (blackOverlay != null)
                {
                    blackOverlay.color = new Color(0, 0, 0, t);
                }
                yield return null;
            }

            // Carga la escena
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            // Wait until the scene is fully loaded
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
