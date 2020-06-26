using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상하좌우로 커맨드에 따라 플레이어가 움직였으면 좋겠다.

public class PlayerMove : MonoBehaviour
{
    // 플레이어가 상하좌우 버튼에 따라 움직이고 싶다
    // 캐릭터가 내가 입력하는 상하좌우 방향을 바라보게 하고 싶다.
    // 플레이어가 스페이스를 누르면 점프하게 하고싶다
    // 쉬프트 키를 누르면 대쉬하고 싶다



    // 점프
    private Transform transform;
    // FloorSensor 스크립트 가져오기, 센서를 통해 땅에 닿았는지 판단
    FloorSensor floorSensor;
    public float jumpPower;
    public bool isJumping;


    public float speed;         // 플레이어 속도

    // 이동 벡터 값
    Vector3 movement;

    // 달리기
    private float runningSpeed;  // 달리기 시 속도 증가 비율

    // 앉기
    private float crouchSpeed;   // 앉았을때 속도 감소 비율
    private float crouchHeight;
    CapsuleCollider capCollider;
    CeilingSensor ceilngSensor;

    // 캐릭터 회전
    private float rotateSpeed; // 회전 스피드 변수
    Rigidbody rigidbody;

    // 사다리
    float ladderSpeed;
    Vector3 ladderDir;
    bool onLadder;



    // Start is called before the first frame update
    void Start()
    {

        // 점프
        transform = base.transform;
        // FloorSensor 스크립트 가져오기
        floorSensor = GameObject.Find("FloorSensor").GetComponent<FloorSensor>();
        isJumping = false;

        // 달리기
        runningSpeed = 1.5f;


        // 앉기
        crouchSpeed = 0.5f;
        crouchHeight = 0.5f;
        capCollider = GetComponent<CapsuleCollider>();
        ceilngSensor = GetComponent<CeilingSensor>();

        // 회전
        rigidbody = GetComponent<Rigidbody>();
        rotateSpeed = 15f;

        // 사다리
        ladderSpeed = 5f;
        onLadder = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!onLadder)
        {

            Run();
            Crouch();
        }
    }

    private void FixedUpdate()
    {
        if (!onLadder)
        {
            Move();
        }
        Jump();
    }

    void Jump()
    {
        // 점프 판정
        // 스페이스를 누르면, 점프 중이 아닐시에 점프 실행
        // if (Input.GetKeyDown(KeyCode.Space) && floorSensor.isGround)
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // 리지드 바디에 힘을 가해서 점프
            rigidbody.AddForce(Vector3.up.normalized * jumpPower, ForceMode.Impulse);

            // 센서가 땅에 닿기까지 점프 횟수를 0으로, 땅에서 떨어졌다고 판정
            // 땅에서 떨어졌다 판정
            floorSensor.isGround = false;
            // 남은 점프 가능 횟수 0회
            // 점프 중 판정
            isJumping = true;
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        isJumping = false;
    }


    void Move()
    {

        // 상하전후 입렧시
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement.Set(h, 0, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);

        //  캐릭터 회전 : 움직임이 없으면 그 방향을 그대로 바라보도록
        if (h == 0 && v == 0)
        {
            return;
        }
        //회전값, movement 벡터 값으로 회전
        Quaternion newRotaion = Quaternion.LookRotation(movement);
        //부드럽게 회전
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, newRotaion, rotateSpeed * Time.deltaTime);

    }

    void Run()
    {
        // 쉬프트를 누르면 대쉬
        // 쉬프트를 누르동안 빨라진다
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= runningSpeed;
            return;
        }
        // 쉬프트를 떼면 원래 속도로 돌아온다
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= runningSpeed;
        }
    }


    void Crouch()
    {
        // 컨트롤 키를 누르면 캐릭터가 앉게 하고 싶다
        // 플레이어 머리위 센서에 오브젝트가 닿아있으면 컨트롤 키를 떼더라도 앉기가 유지되게 하고 싶다
        //  - 컨트롤 키를 떼어도 앉기를 계속한다

        //컨트롤 키를 누르면
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // 속도가 느려진다.
            speed *= crouchSpeed;
            // 캡슐 콜라이더가 작아진다
            capCollider.height *= crouchHeight;
            // 캡슐 콜라이더가 땅위로 옮겨진다
            capCollider.center = new Vector3(capCollider.center.x, capCollider.center.y * crouchHeight, capCollider.center.z);

        }

        //컨트롤 키를 떼면
        // 센서가 천장이 없다고 파악이 되면
        // 일어난다
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            // 플레이어 속도가 돌아온다.
            speed /= crouchSpeed;
            // 캡슐 콜라이더가 원래 위치로 돌아온다
            capCollider.center = new Vector3(capCollider.center.x, capCollider.center.y / crouchHeight, capCollider.center.z);
            // 캡슐 콜라이더 크기가 원래대로 돌아온다
            capCollider.height /= crouchHeight;

        }
    }

    // 사다리

    void OnTriggerStay(Collider other)
    {

        // 만약 플레이어가 사다리에 부딪힌다면 
        if (other.transform.tag == "Ladder")
        {

            // 왼쪽 버튼을 누르는 동안
            if (Input.GetMouseButton(0))
            {
                Debug.Log("1");
                // 사다리 판정 On
                onLadder = true;
                // 사다리에 고정
                rigidbody.position = new Vector3(other.transform.position.x - 0.25f, 0, other.transform.position.z).normalized;
                // 위치 고정, 사다리 붙잡기
                
                // 중력 무시
                rigidbody.useGravity = false;
                // 상하 키를 눌렀을때
                Debug.Log("2");

                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                // 오브젝트의 상하방향으로
                ladderDir = new Vector3(h, v, 0);
                Debug.Log("3");

                // 사다리를 바라본 채로
                transform.forward = -1 * other.transform.forward;
                // 움직인다
                Vector3 ladderMov = ladderDir.normalized * ladderSpeed * Time.deltaTime;
                rigidbody.MovePosition(transform.position + ladderMov);

                Debug.Log("4");
            }
            // 마우스 버튼을 떼면 사다리에서 떨어진다
            else
            {
                // 사다리 모드에서 빠져나오기
                onLadder = false;
                // 중력 복원
                rigidbody.useGravity = true;
                // 사다리 놓기, y 고정 풀기
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            // 사다리 모드에서 빠져나오기             
            onLadder = false;
            // 중력 복원
            rigidbody.useGravity = true;
            // 사다리 놓기, y 고정 풀기
        }
    }

}

