using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    Animator animator;
    public float attackTime;
    public float startTimeAttack;
    public Transform attackLocation;
    public float attackRange;
    public LayerMask enemies;
    Animator enemyAnimator;

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
        if(Input.GetKeyDown("left ctrl"))
        {
            animator.SetTrigger("isAttacking");
        }
        if( attackTime <= 0 )
        {
            if( Input.GetButton("Fire1"))
            {
                Collider2D[] damage = Physics2D.OverlapCircleAll( attackLocation.position, attackRange, enemies );
                for (int i = 0; i < damage.Length; i++)
                {
                    enemy1 other= damage[i].gameObject.GetComponent<enemy1>(); 
                    other.health-=1.0f;
                    screenShakeController.instance.StartShake(0.2f,0.3f);
                }
            }
            attackTime = startTimeAttack;
        }   else
        {
            attackTime -= Time.deltaTime;
        }
        
    }

     private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackLocation.position, attackRange);
    }
}
