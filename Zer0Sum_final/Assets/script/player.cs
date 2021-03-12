using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    float horizontalValue;
    public float speed=1;
    bool faceright=true;
    bool isRunning=false;
     float runSpeed = 1.5f;
    Animator animator;

    [SerializeField] Transform groundcheckObject;
    private float groundcheckRadius=0.04f;
    public bool isgrounded=false;

    [SerializeField] LayerMask groundLayer;
    bool isJumping;
    [SerializeField] public float jumpPower;


    void Awake(){

        //referencing rigidbody of player
        rigidbody=GetComponent<Rigidbody2D>();
        //refrencing player animator to change blend tree value
        animator=GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    void FixedUpdate(){ 
        groundcheck();
        Move(horizontalValue,isJumping);
       
    }
    void groundcheck(){  
           isgrounded=false;
            Collider2D[] collider=Physics2D.OverlapCircleAll(groundcheckObject.position,groundcheckRadius,groundLayer);
            if(collider.Length>0){
                isgrounded=true;
            }     
          
    }
    void Move(float dir,bool jump){

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
}
