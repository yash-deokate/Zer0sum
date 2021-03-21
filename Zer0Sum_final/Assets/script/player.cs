using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    #region variables

    //var for rigidBody
    private Rigidbody2D rigidbody;
     // var for animations
    Animator animator;


    // vars for run and walk
    public float horizontalValue;
    public float speed=1;
    public bool faceright=true;
    bool isRunning=false;
     float runSpeed = 1.5f;



    //vars for jumps
    [SerializeField] Transform groundcheckObject;
    private float groundcheckRadius=0.04f;
    public bool isgrounded=false;
    [SerializeField] LayerMask groundLayer;
    bool isJumping;
    [SerializeField] public float jumpPower;




    // var for health, healthBAr and respawn 
    [SerializeField] public Image health;
    private float healthamnt=10f;
    [SerializeField] Transform respawnPoint;



    // vars for knock back
    [SerializeField] float knockBackLength=0.5f;
    [SerializeField] float knockBackForce=15f;
    bool isHurt=false;

    public bool canMove=true;

   public float thrust;

    #endregion
   
   
   // Runs once when object is created
    void Awake(){

        //referencing rigidbody of player
        rigidbody=GetComponent<Rigidbody2D>();
        //refrencing player animator to change blend tree value
        animator=GetComponent<Animator>();

    }
 
  
    // Update is called once per frame
    // using this for taking inputs
    void Update()
    {
        // left right input
        horizontalValue= Input.GetAxisRaw("Horizontal");

        //run input
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            isRunning=true;
        }
         if(Input.GetKeyUp(KeyCode.LeftShift)){
            isRunning=false;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            //  animator.SetBool("jumping",true);
            isJumping=true;

        }
        else if(Input.GetKeyUp(KeyCode.Space)){
            //  animator.SetBool("jumping",false);
            isJumping=false;
        }
        

    }

    // runs per frame
    // using this to update physics
    void FixedUpdate(){ 
        groundcheck();
        if(canMove){
             Move(horizontalValue,isJumping);
        }
    }
    
    //check if object is in air to avoid jumping when in air
    void groundcheck(){  
           isgrounded=false;
            Collider2D[] collider=Physics2D.OverlapCircleAll(groundcheckObject.position,groundcheckRadius,groundLayer);
            if(collider.Length>0){
                isgrounded=true;
            }     
          
    }


   // jump and moving mechanism
    void Move(float dir,bool jump){

    #region jump
    if(!isgrounded){
        animator.SetBool("jumping",true);
    }
    else if(isgrounded){
        animator.SetBool("jumping",false);
    }
    if(isgrounded && jump){
        isJumping=false;
        float yforce=250*jumpPower;
        if(isRunning){
            yforce+=35f;
        }
        rigidbody.AddForce(new Vector2(0f,yforce));
    }
    #endregion

    #region move and run


        //move mech left right
        float xval=dir*speed*Time.fixedDeltaTime*100;

        //increase input if running
        if(isRunning){
            xval*=runSpeed;
        }

        //move mech left right==> adding velocity to object 
        Vector2 targetVelocity=new Vector2(xval,rigidbody.velocity.y);
        rigidbody.velocity=targetVelocity;

        //flip mech
        if(faceright && dir<0){
            transform.localScale= new Vector3(-1,1,1);
            faceright=false;
        }
        else if(!faceright && dir>0){
             transform.localScale= new Vector3(1,1,1);
            faceright=true;
        }

        // walk animation blend tree output
        animator.SetFloat("xvelocity",Mathf.Abs(rigidbody.velocity.x));
        #endregion
     
    }


    // collision with enemy and other opps
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("enemy")){
            // hurt animation
            animator.SetBool("hit",true);
            //healthbar update
            healthamnt-=1;
            health.fillAmount=healthamnt/10;
            // knckback on enemy attack
            doKnockBack(transform.localScale.x);

            //on dying respawn and health bar update
            if(healthamnt==0){
                transform.position=respawnPoint.position;
                healthamnt=10;
                health.fillAmount=healthamnt/10;
            }
        }
        
    }
   
   // restoring animation after enemy attack
    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.tag.Equals("enemy")){
             animator.SetBool("hit",false);
        }
    }


    // knock back on enemy attack in given direction
    public void doKnockBack(float dir){
        StartCoroutine(DisablePlayerMovement(knockBackLength));
        rigidbody.velocity=new Vector2(-dir*knockBackForce,knockBackForce);
    }

    // coroutine to stop moving for given time so that we can knockback player
    IEnumerator DisablePlayerMovement(float time){
        // we allow move function(in Fixedupdate) only if canMove is true
         canMove=false;
         // there is no use of this var.. just for future purposes
        isHurt=true;
        // this animatio change does not make any sense...just trial 
         animator.SetBool("hit",true);
        yield return new WaitForSeconds(time);
         animator.SetBool("hit",false);
        canMove=true;
        isHurt=false;
    }
   
}
