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

}
