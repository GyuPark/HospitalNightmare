using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 센서가 트리거를 건드리는지를 판단
// 센서 판단

public class EnemySensor : MonoBehaviour
{

    // 센서 판단
    public bool enemyTrigger;

    // Start is called before the first frame update
    void Start()
    {
        enemyTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "EnemyTrigger")
        {
            enemyTrigger = true;
            SpiderAnim.anim.SetTrigger("walk");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyTrigger")
        {
            enemyTrigger = false;
            SpiderAnim.anim.SetTrigger("idle");
        }
    }


}
