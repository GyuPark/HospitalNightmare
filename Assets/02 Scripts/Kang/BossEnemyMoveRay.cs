using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRay : MonoBehaviour
{
    // 이동 포인트 배열로 불러오기 
    public Transform[] points;
    // 포인트 첫 값을 1로
    public int nextIdx = 1;

    // 에너미 트랜스폼 변수 선언
    private Transform tr;
    // 플레이어 트랜스폼 변수 선언
    private Transform playerTr;






    // 에너미 이동 속도
    private float speed = 10f;
    // 회전 이동 속도
    private float damping = 5.0f;

    Rigidbody rigidbody;


    // 시야각
    [SerializeField] float m_angle = 0f;
    // 시야거리
    [SerializeField] float m_distance = 0f;
    // 레이어마스크
    [SerializeField] LayerMask m_layerMask = 0;

    // 플레이어 찾았다고 판단
    public bool findOut = false;


    // Start is called before the first frame update
    void Start()
    {
        // tr 변수 트랜스폼 자료 값 가져오기
        tr = GetComponent<Transform>();
        // 플레이어 위치
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // 이동 포인트 위치
        points = GameObject.Find("BossWayPointGroup").GetComponentsInChildren<Transform>();

        rigidbody =  GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Sight();
    }

         

    // 시야에 들어오면 추적하기 시작
    void Sight()
    {
        // 콜라이더 위치 정보를 받는다
        // Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask);

        // 플레이어 정보를 받는다
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // 플레이어와의 거리 계산
        float dist = Vector3.Distance(transform.position, playerTr.position);
        // 에너미와 플레이어 사이 거리가 인지 거리보다 짧다면
        if (dist < m_distance)
        {
            print("1");
            // Transform t_tfPlayer = t_cols[0].transform;
            // 시야 거리
            Vector3 t_direction = (playerTr.position - transform.position).normalized;
            // 시야 각도
            float t_angle = Vector3.Angle(t_direction, transform.forward);
            // 각도가 시야 각도 보다 짧다면
            if (t_angle < m_angle * 0.5f)
            {
                print("2");
                Debug.DrawLine(transform.position, transform.position + t_direction * m_distance);
                // 레이를 쏜다 (레이 시작위치, 레이의 방향, 레이충돌 반환, 레이 길이값)
                if (Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance))
                {
                    print("3");
                    // 레이에 맞은 오브젝트의 이름이 플레이어라면
                    if (t_hit.transform.name == "Player")
                    {
                        print("4");
                        // 플레이어 방향으로 이동한다
                        // 플레이어 방향
                        Vector3 dir = new Vector3(playerTr.transform.position.x - tr.transform.position.x, 0, playerTr.transform.position.z - tr.transform.position.z);
                        dir.Normalize();
                        // 무브먼트에 벡터 움직임 할당
                        Vector3 movement = dir * speed * 3 * Time.deltaTime;
                        // 리디즈 바디 움직임
                        rigidbody.MovePosition(transform.position + movement);

                        // 회전
                        Quaternion rot = Quaternion.LookRotation(dir);
                        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
                        findOut = true;
                    }
                    else
                    {
                        Move();
                    }

                }
                else
                {
                    Move();
                }
            }
            else
            {
                Move();
            }

        }        
        else
        {
            Move();
        }
    }

    void Move()
    {
        // 에너미 위치 벡터값, x z 정보만
        Vector3 enemyPos = new Vector3(tr.position.x, 0, tr.position.z);
        // 포인트 위치 벡터값, x, z 정보만
        Vector3 pointPos = new Vector3(points[nextIdx].position.x, 0, points[nextIdx].position.z);
        // 포인트 방향으로 향하는 벡터값을 구하고
        Vector3 movement = pointPos - enemyPos;
        // 무브먼트에 스피드와 시간 더해서 움직임 적용
        movement = movement.normalized * speed * Time.deltaTime;
        // 리지드 바디 움직이기
        rigidbody.MovePosition(transform.position + movement);

        // 회전
        Quaternion rot = Quaternion.LookRotation(pointPos - enemyPos);
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * damping);
    }

    // 포인트에 부딪혔을 시 다음 포인트 이동
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "WAY_POINT")
        {
            // 다음 포인트로 이동한다
            // 포인트 끝으로 이동시 첫 포인트로 방향을 바꾼다
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
        }
    }


    // 플레이어와 닿으면 플레이어가 죽는다
    private void OnCollisionEnter(Collision other)
    {
        // 플레이어가 죽는다
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            GameManager.instance.GameOver = true;

        }
    }


}
