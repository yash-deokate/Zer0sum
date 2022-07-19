using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject dice;

    private void OnEnable() {
        gameOver.OnGameOver += EnableGameOverMenu;
    }
    private void OnDisable() {
        gameOver.OnGameOver -= EnableGameOverMenu;
    }

    public void EnableGameOverMenu(){
        gameOverMenu.SetActive(true);
        // dice.SetActive(false);
    }

     public void RestartGame(){
        SceneManager.LoadScene("level0");
    }
     public void QuitGame(){
       SceneManager.LoadScene("StartMenu");
     }
}
