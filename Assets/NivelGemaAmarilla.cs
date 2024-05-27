using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NivelGemaAmarilla : MonoBehaviour
{
    public TextMeshProUGUI textoGemaAmarilla1;
    public TextMeshProUGUI textoGemaAmarilla2;
    public TextMeshProUGUI textoGemaAmarilla3;
    public TextMeshProUGUI textoGemaAmarilla4;
    public TextMeshProUGUI textoGemaAmarilla5;
    public TextMeshProUGUI textoGemaAmarilla6;
    public TextMeshProUGUI textoGemaAmarilla7;

    void Start()
    {
        if (PlayerPrefs.GetInt("GemaAmarilla0") == 1)
        {
            textoGemaAmarilla1.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla1.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla1") == 1)
        {
            textoGemaAmarilla2.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla2.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla2") == 1)
        {
            textoGemaAmarilla3.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla3.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla3") == 1)
        {
            textoGemaAmarilla4.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla4.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla4") == 1)
        {
            textoGemaAmarilla5.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla5.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla5") == 1)
        {
            textoGemaAmarilla6.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla6.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }

        if (PlayerPrefs.GetInt("GemaAmarilla6") == 1)
        {
            textoGemaAmarilla7.fontSharedMaterial.SetFloat("_GlowPower", 0.3f);
        }
        else
        {
            textoGemaAmarilla7.fontSharedMaterial.SetFloat("_GlowPower", 0.0f);
        }




    }
}
