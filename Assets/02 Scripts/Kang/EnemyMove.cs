using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 트리거를 발동시키면 일정 시간 이후 플레이어를 따라왔으면 좋겠다.
// 트리거
// 멈춤

public class EnemyMove : MonoBehaviour
{
    // 타겟
    public Transform target;
    // 방향
    private Vector3 direction;
    // 속도
    float speed = 9.2f;
    // 거리
    // float distance;
    // float enemyDistance = 30;

    // 경과시간
    float currenTime;
    //추적 실행 시간
    float chaseingInitiateTime = 0.5f;    

    //EnemySensor판단
    public EnemySensor ES;


    // Start is called before the first frame update
    void Start()
    {
        // EnemySensor의 스크립트를 ES에 할당
        ES = GameObject.Find("EnemySensor").GetComponent<EnemySensor>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        // Player의 현재 위치를 받아오는 Object
        target = GameObject.Find("Player").transform;
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화한다.
        direction = (target.position - transform.position).normalized;    
        // Player와 객체 간의 거리 계산
        // distance = Vector3.Distance(target.position, transform.position);
        // 플레이어 방향
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();
        // 일정거리 안에 있을 시, 해당 방향으로 무빙
        // if (distance <= enemyDistance)
        // 에너미 센서가 감지되기 시작하면
        if (ES.enemyTrigger == true)
        {
            // 시간이 흐르기 시작
            currenTime += Time.deltaTime;
            if (currenTime > chaseingInitiateTime)
            {
                // 움직인다
                this.transform.position += dir * speed * Time.deltaTime;
            }
        }
    }


    // 플레이어와 닿으면 플레이어가 죽는다
    private void OnCollisionEnter(Collision other)
    {
        // 플레이어가 죽는다
        if (other.gameObject.tag == "Player")
        {
            SpiderAnim.anim.SetTrigger("attack");
            Destroy(other.gameObject);
            GameManager.instance.GameOver = true;
        }
    }
}
