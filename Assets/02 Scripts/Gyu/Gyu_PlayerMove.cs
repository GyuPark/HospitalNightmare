using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//목표1 : 사용자가 입력하는 방향이 Player의 로컬 앞이 되어 이 방향대로 전진한다.
//3. 사용자 입력하면
//2. 앞을 보고 (transform.forward 방향으로)
//1. 전진한다 (언제? 어디서? 어떻게?) P = P0 +vt
//필요속성 : 속력, 방향

public class Gyu_PlayerMove : MonoBehaviour
{
    public float speed = 4;
    Vector3 userDir;
    float h;
    float v;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //3. 사용자 입력하는 상태라면
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.anyKey)
        {
            // - 사용자 입력 방향.
            userDir = Vector3.right * h + Vector3.forward * v;
            userDir.Normalize();

            //2. 앞을 보고 (transform.forward 방향으로)
            transform.forward = userDir;

            //1. 전진한다 
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
