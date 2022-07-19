using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
   
    private Rigidbody2D rb;
    public float speed;
    float moveDirection = 0;
    bool facingRight = true;
    Transform t;
    private Animator animator;
    BoxCollider2D mainCollider;
    bool isGrounded = false;
    public float jumpHeight = 6.5f;


    void Start()
    {
        t = transform;
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        mainCollider = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        // if(other.gameObject.tag == "ground"){
        //     Destroy(this.gameObject);
        //     // Debug.Log("Game Over");
        // }

        if(other.gameObject.tag == "point"){
            Destroy(other.gameObject);
            scoreManager.instance.addScore();
        }

        // if(other.gameObject.tag == "sky"){
        //     Debug.Log("Game Over");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        // {
        //     moveDirection = Input.GetKeyDown(KeyCode.A) ? -1 : 1;
        //     animator.SetBool("run", true);
        // }
        // else if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)))
        // {
        //     moveDirection = 0;
        //     animator.SetBool("run", false);
        // }

         if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && (isGrounded || Mathf.Abs(rb.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;
            animator.SetBool("run", true);
        }
        else
        {
            if (isGrounded || rb.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
                animator.SetBool("run", false);
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            // diceController.instance.roll();
        }

        if(Input.GetKey(KeyCode.UpArrow) && isGrounded){
            diceController.instance.roll();
        }

        if(isGrounded){
            animator.SetBool("jump", false);
        }
        else{
            animator.SetBool("jump", true);
            
        }
    }

    void FixedUpdate() {
        
        rb.velocity = new Vector2((moveDirection) * speed, rb.velocity.y);

        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    // void OnTriggerStay2D(Collider2D obj)
    // {
    //     if (obj.CompareTag("dice"))
    //     {
    //         rb.drag = 1f;

    //         float distance = Mathf.Abs(obj.GetComponent<gravityPoint>().planetRadius - Vector2.Distance(transform.position, obj.transform.position));
    //         if (distance < 1f)
    //         {
    //             isGrounded = distance < 0.5f;
    //         }
    //     }
    // }

    // void OnTriggerExit2D(Collider2D obj)
    // {
    //     if (obj.CompareTag("dice"))
    //     {
    //         rb.drag = 0.2f;
    //     }
    // }

    
}
