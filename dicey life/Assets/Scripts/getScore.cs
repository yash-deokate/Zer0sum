using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour
{
    public Text ScoreText;
    int score=0;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        ScoreText.text = score.ToString();
        
    }
}
