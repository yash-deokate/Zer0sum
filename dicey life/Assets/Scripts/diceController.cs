using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceController : MonoBehaviour
{
    public static diceController instance;
    private Animator animator;
    int randomSide=0;

    private void Awake() {
        instance =this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void roll(){
        animator.SetBool("roll", true);
        randomSide = Random.Range(1,6);
        Invoke("showSide", 0.5f);
    }

    public void showSide(){
        foreach(AnimatorControllerParameter parameter in animator.parameters) {            
            animator.SetBool(parameter.name, false);            
        }
        // animator.SetBool("roll", false);
        // animator.SetBool("2", true);
        switch (randomSide)
        {
        case 6: animator.SetBool("6", true);
            break;
        case 5:
            animator.SetBool("5", true);
            break;
        case 4:
            animator.SetBool("4", true);
            break;
        case 3:
            animator.SetBool("3", true);
            break;
        case 2:
            animator.SetBool("2", true);
            break;
        case 1:
            animator.SetBool("1", true);
            break;
        default:
            animator.SetBool("1", true);
            break;
        }
    }

    public int getSide(){
        return randomSide;
    }

    public void GameOver(){
        foreach(AnimatorControllerParameter parameter in animator.parameters) {            
            animator.SetBool(parameter.name, false);            
        }
        animator.SetBool("gameOver", true);
    } 
}
