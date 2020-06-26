using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.GameOver = true;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        GameManager.instance.GameOver = true;
    //    }
    //}

}
