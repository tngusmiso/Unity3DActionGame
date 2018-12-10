using UnityEngine;
using System.Collections;

public class CharaAnimation : MonoBehaviour {

    Animator animator;
    CharacterStatus status;
    Vector3 prePosition;
    bool isDown=false;
    bool attacked = false;
    bool jumped = false;

    public bool IsAttacked(){
        return attacked;
    }
    public bool IsJumped(){
        return jumped;
    }
    void StartAttackHit(){
        Debug.Log("StartAttackHit");
    }
    void EndAttackHit(){
        Debug.Log("EndAttackHit");
    }
    void EndAttack(){
        attacked = true;
    }
    void EndJump(){
        jumped = true;
    }
    void Start(){
        animator = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();

        prePosition = transform.position;
    }

	void Update () {

        Vector3 delta_position = transform.position - prePosition;
        animator.SetFloat("Speed", delta_position.magnitude / Time.deltaTime);

        if(attacked && !status.attacking){
            attacked = false;
        }
        animator.SetBool("Attacking", (!attacked && status.attacking));
        
        if(jumped && !status.jumping){
            jumped = false;
        }
        animator.SetBool("Jumping", (!jumped && status.jumping));
       
        if(!isDown&&status.died){
            isDown = true;
            animator.SetTrigger("Down");
        }

        prePosition = transform.position;
   }
}