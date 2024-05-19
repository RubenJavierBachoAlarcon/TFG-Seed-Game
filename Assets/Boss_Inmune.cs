using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Inmune : MonoBehaviour
{
    public static bool isInmune = false;
    // Start is called before the first frame update
    void Start()
    {
        isInmune = false;
    }

    void ActivateInmune()
    {
        isInmune = true;
    }

    void DeactivateInmune()
    {
        isInmune = false;
    }

    void changeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, 0.5f);
    }
}
