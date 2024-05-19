using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class End_Boss_Battle : MonoBehaviour
{
    public PauseMenu pauseMenu;
    
    public TextMeshProUGUI scoreText;
    void pauseTimer()
    {
        pauseMenu.pauseTimer();
        PlayerPrefs.SetInt("Nivel 2 - Montaña", (int)(100000 / pauseMenu.GetGameTime()));
        
    }
    void cutSceneFinal()
    {
        FadeManager.Instance.FadeToScene("Cutscene Final");
    }
}
