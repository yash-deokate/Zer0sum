using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject bullet;
    public float Bulletspeed;
    public Transform shootPoint;
    public Transform player;
    public float fireRate;
    float readyforNextshot;
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
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("firing",true);
            if(Time.time>readyforNextshot){
                readyforNextshot=Time.time+1/fireRate; 
                shoot();
            }   
        }
        if(Input.GetButtonUp("Fire1")){
             animator.SetBool("firing",false);
        }
    }


    void shoot(){
         GameObject instantiatedProjectile=Instantiate(bullet,shootPoint.position,shootPoint.localRotation);
         float dir=player.transform.localScale.x;
         if(dir==1){
             instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(instantiatedProjectile.transform.right*Bulletspeed*0.1f);
         }
         else{
             instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(instantiatedProjectile.transform.right*Bulletspeed*-1*0.1f);
         }
         Destroy(instantiatedProjectile,1);
    }
}
