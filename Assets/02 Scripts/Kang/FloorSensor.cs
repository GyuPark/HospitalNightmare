using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어가 떵에 닿았는지 아닌지를 파악한다

public class FloorSensor : MonoBehaviour
{
    // 필요속성 : 점프 판정
    public bool isGround;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 센서가 땅에 닿아서 트리거가 작동되면
    private void OnTriggerEnter(Collider other)
    {
        // 오브젝트에 부딪힌다면, 오브젝트 중 플레이어 태그 오브젝트는 제외
        if ((other.gameObject.tag != "Player") && (other.gameObject.layer != LayerMask.NameToLayer("Sensor")))
        {
            // 점프 판정을 가능하도록 한다, 땅에 닿았다고 판단
            isGround = true;
        }
    }


    // 땅에서 떨어지면 땅에 없다고 판정한다
    private void OnTriggerExit(Collider other)
    {
        // 오브젝트에 부딪힌다면, 오브젝트 중 플레이어 태그 오브젝트는 제외
        if ((other.gameObject.tag != "Player") && (other.gameObject.layer != LayerMask.NameToLayer("Sensor")))
        {
            // 점프 판정을 불가능하도록 한다, 땅에서 떨어졌다고 판단
            isGround = false;
        }
    }

}
