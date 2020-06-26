using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 : 카메라는 항상 FocalPoint를 바라보며, cam_offset만큼 떨어져있다.
//목표 : pos 위치가 변할 땐 lerp로 부드럽게 이동한다.

public class Gyu_CameraMove : MonoBehaviour
{
    Transform focalPoint;
    public Gyu_Sensor sensor;
    Vector3 pos;
    Vector3 cam_offset;

    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point").transform;
        cam_offset = sensor.newCamOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Idle();
    }

    void Move()
    {
        //1. 항상 focal point를 바라본다
        Vector3 dir = focalPoint.position - transform.position;
        dir.Normalize();
        transform.forward = dir;

        //2. 언제나 특정 offset만큼 focal point에서 떨어져있다.
        //camoffset은 region마다 달라지는데, region마다 달라지는 newCamOffset은 sensor에서 
        //결정한다. sensor에서 지정되는 새로운 offset으로 부드럽게 이동한다.
        cam_offset = Vector3.Lerp(cam_offset, sensor.newCamOffset, Time.deltaTime);

        //noclamp??
        //pos = focalPoint.position + cam_offset;
        //transform.position = pos;

        //clamp!!
        pos = focalPoint.position + cam_offset;
        float posZ = pos.z;
        float clampedZ = Mathf.Clamp(posZ ,sensor.min, sensor.max);
        Vector3 clampedPos = new Vector3(pos.x, pos.y, clampedZ);
        //transform.position = clampedPos;
        transform.position = Vector3.Lerp(transform.position, clampedPos, Time.deltaTime);

    }

    //목표 : UserInput에 의한 Player의 움직임이 없을 땐, z축을 중심으로 시소운동한다.
    //1. 시소 운동한다
    //2. 어떻게? z 축을 중심으로
    //3. 어떤 속도로? 천천히
    void Idle()
    {
        //4. userinput 없을 때
        if (Input.anyKey == false)
        {
            //Quaternion 배우면 하자 ... 삽질하지말고ㅠㅠ
        }
    }
}
