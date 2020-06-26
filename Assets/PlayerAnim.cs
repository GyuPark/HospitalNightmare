using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Debug.Log(Input.GetMouseButton(0));

        if ((h != 0 || v != 0) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("run");
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("sit");
        }
        else if ((h != 0 || v != 0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetTrigger("walk");
        }
        else if ((h != 0 || v != 0) && Input.GetMouseButton(0))
        {
            
            anim.SetTrigger("climb");
        }
        else
        {
            anim.SetTrigger("idle");
        }
    }
}
