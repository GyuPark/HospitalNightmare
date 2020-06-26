using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    // 플레이어와 접촉한 뒤에 계속 떨어지고 싶다
    // 플레이어와 접촉
    // 지속적으로 떨어지는 운동

    // a떨어지는 속도
    private float speed = 30;
    private bool touch = false;
    private float fallingtime = 0.25f;
    private float currentime;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Falling();
        if (touch == true)
        {
            currentime += Time.deltaTime;

        }
    }

    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.tag == "Player")
        {
            touch = true;

        }
    }

    private void Falling()
    {
        if (touch == true && currentime > fallingtime)
        {
            Vector3 dir = Vector3.down;
            transform.position += dir * speed * Time.deltaTime;
        }
    }

}
