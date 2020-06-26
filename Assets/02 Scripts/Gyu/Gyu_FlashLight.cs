using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//f키로 끄고 키는 손전등

public class Gyu_FlashLight : MonoBehaviour
{
    public GameObject flash;

    // Start is called before the first frame update
    void Start()
    {
        flash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flash.SetActive(!flash.activeSelf);
        }
        
    }
}
