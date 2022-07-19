using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoreManager : MonoBehaviour
{
    public static scoreManager instance;
    public Text scoreText;
    public Text highScoreText;
    int score=0;
    int highScore=0;

    private void Awake() {
        instance =this;
    }


    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
        scoreText.text = score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
        
    }

    public void addScore(){
        score += 1;
        scoreText.text = score.ToString();
        if(highScore < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            
        }
        PlayerPrefs.SetInt("score", score);
    }

    // Update is called once per frame
    
}
