using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOver : MonoBehaviour
{
    public static event Action OnGameOver;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            Destroy(other.gameObject);
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
            diceController.instance.GameOver();
        }

        if(other.gameObject.tag == "sdice" && this.gameObject.tag == "sky"){
            Destroy(player);
            Debug.Log("Game Over");
            OnGameOver?.Invoke();
            diceController.instance.GameOver();
        }

        // if(other.gameObject.tag == "sky"){
        //     Debug.Log("Game Over");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
