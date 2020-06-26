using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라가 항상 바라보는 FocalPoint.
//목표 : Player와 일정한 거리를 유지하며 Player가 움직일 때마다 부드럽게 움직인다.

public class Gyu_FocalPoint : MonoBehaviour
{
    Transform player; //player
    [HideInInspector]
    public Vector3 focal_offset; //일정한 거리
    float speed = 1; //Idle 상태에서 움직이는 속도
    Vector3 pos; //새로운 위치

    public Gyu_Sensor sensor; //센서



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        
        //4. 태어났을 때의 거리.
        focal_offset = new Vector3(1.91f, 0, 0);
        pos = player.position + focal_offset;
        transform.position = pos;
    }

    void Update()
    {
        Idle();
        Move();
    }

    //목표 : User가 움직일 때의 FocalPoint 움직임
    // - sensor가 새로운 지역을 감지할 때마다 새로운 위치를 받아와 부드럽게 이동한다.
    void Move()
    {
        //1. 움직인다
        //2. player와 관련해 특정 포지션 pos로
        focal_offset = sensor.newFocalOffset;
        pos = player.position + focal_offset;
        //3. 부드럽게 현재의 위치 transform.position 에서 새로운 위치 pos로 이동
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }


    // 목표1.1 : UserInput이 없을 때는 배가 파도에 떠있는 것처럼 수직운동하고, 
    void Idle()
    {
        // 1.1.4 언제? 유저가 아무것도 누르지 않으면
        if (Input.anyKey == false)
        {
            // 1.1.3 어떻게? speed 가 sine함수의 y축을 따라
            speed = Mathf.Sin(Time.time / 2);

            // 1.1.2 어디로? 아래 위로
            Vector3 dir = Vector3.up;

            // 1.1.1 움직인다 P = P0 +vt
            transform.position += dir * speed * Time.deltaTime;
        }
    }
}
