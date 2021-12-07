using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
       public GameObject bullet;
    public float Bulletspeed;
    public Transform shootPoint;
    public Transform players;
     float amoAmount;
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
        amoAmount=player.bulletsAmount;
        if (Input.GetButton("Fire1") && amoAmount>0)
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
         GameObject instantiatedProjectile=Instantiate(bullet,shootPoint.position,shootPoint.rotation);
         float dir=players.transform.localScale.x;
         if(dir>0){
             instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(instantiatedProjectile.transform.right*Bulletspeed*0.1f);
         }
         else if(dir<0){
             instantiatedProjectile.GetComponent<Rigidbody2D>().AddForce(-instantiatedProjectile.transform.right*Bulletspeed*0.1f);
         }
         player.bulletsAmount=player.bulletsAmount-1;
         Destroy(instantiatedProjectile,1);

    }
}
