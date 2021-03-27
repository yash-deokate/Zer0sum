using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour
{
    [SerializeField] Transform target;
    public float speed;
    public float lineOfSite;
     Animator animator;
      private Rigidbody2D rb;

      Vector2 currentPosition;
      Vector2 newPosition;
      bool canWalk=true;
    // Start is called before the first frame update

    void Awake(){
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        float distanceFromTarget=Vector2.Distance(target.position,transform.position);
        currentPosition=transform.position;
        if(distanceFromTarget< lineOfSite && canWalk){
            transform.position=Vector2.MoveTowards(this.transform.position,new Vector2(target.position.x,transform.position.y),speed*Time.fixedDeltaTime);
        }
        newPosition=transform.position;
        rb.velocity=(newPosition - currentPosition) / Time.fixedDeltaTime;
        animator.SetFloat("xval",Mathf.Abs(rb.velocity.x));
        spriteFlip(target.position.x-transform.position.x);
    }

    void spriteFlip(float dir){
        if(dir<0.0f){
            //flip right;
            // transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localScale=new Vector3(-1,1,1);
        }
        else if(dir>0.0f){
            //flip left;
            // transform.rotation = Quaternion.identity;
            transform.localScale=new Vector3(1,1,1);
        }
    }


    private void OnDrawGizmosSelected(){
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
    }


    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("bullet")){
            canWalk=false;
            Destroy(col.gameObject);
            animator.SetBool("dead",true);
            Destroy(gameObject,0.8f);
        }
    }
}
