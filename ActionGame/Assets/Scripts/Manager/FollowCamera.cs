using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public float distance = 5.0f;
    public float horizontalAngle = 0.0f;
    public float rotAngle = 100.0f;
    public float verticalAngle = 10.0f;
    public Transform lookTarget;
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Vector3 offset = Vector3.zero;
    public bool arrived = true;
    Vector3 velocity = Vector3.zero;

    InputManager inputManager;
    LevelManager levelManager;
    CharacterController characterController;
   // Use this for initialization
   void Start () {
        inputManager = FindObjectOfType<InputManager>();
        levelManager = FindObjectOfType<LevelManager>();
        characterController = GetComponent<CharacterController>();
   }

    void LateUpdate()
    {
        if(inputManager.Moved())
        {
            float anglePerPixel = rotAngle / (float)Screen.width;
            Vector2 delta = inputManager.GetDeltaPosition();
            horizontalAngle += delta.x * anglePerPixel;
            horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
            verticalAngle -= delta.y + anglePerPixel;
            verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
        }   
        
        if(levelManager.levelUpFrom!=0){
            arrived=false;
            FollowGate();
        }
        else if(lookTarget != null)
        {
            Vector3 lookPosition = lookTarget.position + offset;
            Vector3 relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);

            transform.position = lookPosition + relativePos;
            transform.LookAt(lookPosition);

            RaycastHit hitInfo;
            if (Physics.Linecast(lookPosition, transform.position, out hitInfo, 1 <<
                LayerMask.NameToLayer("Ground")))
                transform.position = hitInfo.point;

        }
    }
    // Update is called once per frame
    void Update () {
   
   }

   void FollowGate(){
       Transform target;
        if(levelManager.levelUpFrom == 1 && target1!=null){
            target = target1;
        } else if(levelManager.levelUpFrom == 2 && target2!=null){
            target = target2;
        }else if(levelManager.levelUpFrom == 3 && target3!=null){
            target = target3;
        }
        else return;

        Vector3 destination = target.position;
        Vector3 destinationXZ = destination;
            // 목적지와 현재 위치 높이를 똑같이 한다.
            destinationXZ.y = transform.position.y;

            //**여기서 부터 XZ만 생각한다. */
            // 목적지까지 거리와 방향을 구한다.
            Vector3 direction = (destinationXZ - transform.position).normalized; //dirction  unitvector
            float distance = Vector3.Distance(transform.position, destinationXZ);

            // 현재 속도를 보관한다.
            Vector3 currentVelocity = velocity;

            // 목적지에 가까이 왔으면 도착
            if (arrived || distance < 0.2f)
                arrived = true;

            // 이동 속도를 구한다.
            if (arrived)
                velocity = Vector3.zero;
            else
                velocity = direction * 60.0f; // not arrived -> walk again

            transform.LookAt(target.parent);

            // 부드럽게 보간 처리
            velocity = Vector3.Lerp(currentVelocity, velocity, Mathf.Min(Time.deltaTime * 5.0f, 1.0f)); //Min never up to the 1   t->  0~1                                                                                 // Lerp(a,b,t)      at+(1-t)b      = a~b value saved
            velocity.y = 0;

            Vector3 snapGround = Vector3.zero;
            if (characterController.isGrounded)
                snapGround = Vector3.down;

            // if(status.jumping) Jump();

            // CharacterController를 사용해서 움직인다.
            characterController.Move(velocity * Time.deltaTime + snapGround); // finally player moved
            if (characterController.velocity.magnitude < 0.1f)
                arrived = true;
   }
}