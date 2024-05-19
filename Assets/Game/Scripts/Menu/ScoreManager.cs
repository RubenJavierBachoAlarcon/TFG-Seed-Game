using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int HighScore1;

    public int HighScore2;

    public int HighScore3;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        HighScore1 = 0;
        HighScore2 = 0;
        HighScore3 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Valor de la variable en ChangeScene: " + HighScore1);
        //Para obtener la variable de change scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ChangeScene.Instance != null)
            {
                // Obtén la variable de ChangeScene
                HighScore1 = ChangeScene.Instance.GetSomeVariable();
                Debug.Log("Valor de la variable en ChangeScene: " + HighScore1);
            }
            else
            {
                Debug.LogWarning("ChangeScene Instance no está disponible.");
            }
        }
    }
}
