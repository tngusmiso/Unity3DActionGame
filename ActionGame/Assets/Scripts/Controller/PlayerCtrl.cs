using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {

    const float RayCastMaxDistance = 100.0f;
    CharacterStatus stataus;
    CharaAnimation charaAnimation;
    Transform attackTarget;
    InputManager inputManager;
    public float attackRange = 1.5f;

    enum State{
        Walking, Attacking, Died, Jumping,
    };

    //현재 스테이트
    State state = State.Walking;
    //다음 스테이트
    State nextState = State.Walking;

    // Use this for initialization
    void Start () {
        stataus = GetComponent<CharacterStatus>();
        charaAnimation = GetComponent<CharaAnimation>();
        inputManager = FindObjectOfType<InputManager>();
    }

    void Update(){
        switch(state){
        case State.Walking:
            Walking();
            break;
        case State.Attacking:
            Attacking();
            break;
        case State.Jumping:
            Jumping();
            break;
        }

        if(state!=nextState){
            state = nextState;
            switch(state){
            case State.Walking:
                WalkStart();
                break;
            case State.Attacking:
                AttackStart();
                break;
            case State.Jumping:
                JumpStart();
                break;
            case State.Died:
                Died();
                break;
            }
        }
    }

    void ChangeState(State nextState){
        this.nextState = nextState;
    }
    void WalkStart (){
        StateStartCommon();
    }
    void Walking(){
        if(inputManager.isSpaced()){
            SendMessage("Jump");
            ChangeState(State.Jumping);
        }
        if(inputManager.Clicked()){
            Vector2 clickPos = inputManager.GetCursorPosition();
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray,out hitInfo,RayCastMaxDistance,(1 <<LayerMask.NameToLayer("Ground"))|(1 <<LayerMask.NameToLayer("EnemyHit")))){
                // 지면이 클릭되었다.
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground")){
                    SendMessage("SetDestination", hitInfo.point);
                    ChangeState(State.Walking);
                }

                // 적이 클릭되었다.
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHit")){
                    Vector3 hitPoint = hitInfo.point;
                    hitPoint.y = transform.position.y;
                    float distance = Vector3.Distance(hitPoint,transform.position);
                    if(distance < attackRange){
                        //공격
                        attackTarget = hitInfo.collider.transform;
                        ChangeState(State.Attacking);
                    } else{
                        SendMessage("SetDestination",hitInfo.point);
                    }
                }
            }
        }
    }

    // 공격 스테이트가 시작되기 전에 호출된다.
    void AttackStart(){
        StateStartCommon();
        stataus.attacking = true;

        // 적 방향으로 돌아보게 한다.
        Vector3 targetDirection = (attackTarget.position - transform.position).normalized;
        SendMessage("SetDirection",targetDirection);

        // 이동을 멈춘다.
        SendMessage("StopMove");
    }

    // 공격 중 처리
    void Attacking(){
        if(charaAnimation.IsAttacked())
            ChangeState(State.Walking);
    }

    // 점프 스테이트가 시작되기 전에 호출
    void JumpStart(){
        StateStartCommon();
        stataus.jumping = true;
    }

    // 점프 중 처리
    void Jumping(){
        if(charaAnimation.IsJumped()){
           ChangeState(State.Walking);
        } 
    }

    void Died(){
        stataus.died = true;
    }

    void Damage(AttackArea.AttackInfo attackInfo){
        stataus.HP -= attackInfo.attackPower;
       
        if(stataus.HP <= 0){
            stataus.HP = 0;

            //체력이 0이므로 사망 스테이투로 전환
            ChangeState(State.Died);
        }
    }

    // 스테이트가 시작되기 전에 status를 초기화한다.
    void StateStartCommon(){
        stataus.attacking = false;
        stataus.jumping = false;
        stataus.died = false;
    }
}