using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class End_Boss_Battle : MonoBehaviour
{
    public PauseMenu pauseMenu;
    
    public TextMeshProUGUI scoreText;

    public Light2D spotlight;
    public float spotlightCloseSpeed = 1f;
    void pauseTimer()
    {
        StartCoroutine(CloseSpotlight());
        pauseMenu.pauseTimer();
        PlayerPrefs.SetInt("Nivel 2 - Montaña", (int)(100000 / pauseMenu.GetGameTime()));
    }
    void cutSceneFinal()
    {
        FadeManager.Instance.FadeToScene("Cutscene Final", 5);
    }

    IEnumerator CloseSpotlight()
    {
        while (spotlight.pointLightOuterRadius > 0)
        {
            spotlight.pointLightOuterRadius -= spotlightCloseSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
