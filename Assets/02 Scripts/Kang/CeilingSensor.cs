using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 머리위 센서에 오브젝트가 닿아있으면 컨트롤 키를 떼더라도 앉기가 유지되게 하고 싶다
// 센서 트리거 감지
// 앉기 상태인지 파악
// 센서가 동작할경우 앉은 상태를 유지하도록

public class CeilingSensor : MonoBehaviour
{
    // 앉기 가능한지 파악
    public bool underCeiling;
    
    
    // Start is called before the first frame update
    void Start()
    {
        underCeiling = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 
    private void OnTriggerEnter(Collider other)
    {
        // 만일 머리의 센서 트리거가 오브젝트에 부딪힌다면
        if (other.gameObject && other.gameObject.tag != "Player")
        {
            // 천장이 머리 위에 있다고 파악
            underCeiling = true;
            print("0000");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 트리거가 센서가 오브젝트에 부딪히지 않으면
        if (other.gameObject && other.gameObject.tag != "Player")
        {
            // 천장이 머리 위에 없다고 파악
            underCeiling = false;
            print("1111111");

        }
    }
}
