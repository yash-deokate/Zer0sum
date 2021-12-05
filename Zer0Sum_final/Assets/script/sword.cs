using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake(){
         animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            animator.SetTrigger("isAttacking");
        }
        if(Input.GetButtonUp("Fire1")){
            // animator.ResetTrigger("isAttacking");
        }
    }
}
