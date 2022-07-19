using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    public Text highScoreText;
    int highScore=0;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
        
    }
}
