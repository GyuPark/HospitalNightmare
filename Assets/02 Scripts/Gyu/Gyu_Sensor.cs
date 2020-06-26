using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//목표 : 새로운 지역을 감지하고, 지역마다 다른 카메라와 Focal의 거리를 저장한다.
public class Gyu_Sensor : MonoBehaviour
{
    public Gyu_FocalPoint focalPoint;
    public Gyu_CameraMove cameraMov;

    [HideInInspector]
    public Vector3 newCamOffset; //Camera_move로 전달할 새로운 cam 거리
    [HideInInspector]
    public Vector3 newFocalOffset; //FocalPoint로 전달할 새로운 focal 거리
    bool rapid; //빠른vs느린 카메라 변화 구간 판별

    Vector3 stageCamOffset; //구간마다 변화하는 새로운 cam거리
    Vector3 stageFocalOffset; //구간마다 변화하는 새로운 Focal거리

    [HideInInspector]
    public float min, max; //CameraMove로 전달할 새로운 z축 min max 값
    float stageMin, stageMax; //구간마다 다른 z축 min max Z값

    void Start()
    {
        newFocalOffset = new Vector3(1.91f, 0, 0);
        newCamOffset = new Vector3(0, 8.6f, -12.81f);
        stageFocalOffset = new Vector3(1.91f, 0, 0);
        stageCamOffset = new Vector3(0, 8.6f, -12.81f);

        stageMin = -22f;
        stageMax = 30f;
        min = -22f;
        max = 30f;
    }

    void Update()
    {
        //느린 변화 구간에서는 거리가 천천히 변하고
        if (!rapid)
        {
            newCamOffset = Vector3.Lerp(newCamOffset, stageCamOffset, Time.deltaTime);
            newFocalOffset = Vector3.Lerp(newFocalOffset, stageFocalOffset, Time.deltaTime);
        }
        //빠른 변화 구간에서는 빨리 변한다
        else
        {
            newCamOffset = stageCamOffset;
            newFocalOffset = stageFocalOffset;
        }

        //z축 최소최대값은 언제나 천천히 변한다.
        min = Mathf.MoveTowards(min, stageMin, Time.deltaTime);
        max = Mathf.MoveTowards(max, stageMax, Time.deltaTime);
    }

    //새로운 구간을 감지할 때마다 변화되는 카메라와 focal 거리
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);

        if (other.tag == "START")
        {
            rapid = false; //느린구간
            stageFocalOffset = new Vector3(1.91f, 0, 0);
            stageCamOffset = new Vector3(0, 8.6f, -12.81f);
            stageMin = -22f;
            stageMax = 30f;
        }

        else if (other.tag == "STAIRS")
        {
            rapid = true;
            stageFocalOffset = new Vector3(2f, 0, 0);
            stageCamOffset = new Vector3(0f, 6f, -7f);
            stageMin = -26f;
            stageMax = 30f;
        }
        else if (other.tag == "LONGSTAIRS")
        {
            rapid = true;
            stageFocalOffset = new Vector3(0.5f, 0, 0);
            stageCamOffset = new Vector3(0f, 3f, -55f);
            stageMin = -100f;
            stageMax = 30f;
        }
        else if (other.tag == "TUNNEL")
        {
            rapid = false; //터널을 지나는 구간은 빠른 구간
            stageFocalOffset = new Vector3(0.5f, 0, 0);
            stageCamOffset = new Vector3(0f, 3.5f, -9f);
        }
        
        else if (other.tag == "LOOSESTEPS")
        {
            rapid = false;
            stageFocalOffset = new Vector3(8f, 0, 0);
            stageCamOffset = new Vector3(-2f, 6f, -35f);
        }

        else if (other.tag == "SEESAW")
        {
            rapid = false;
            stageFocalOffset = new Vector3(3f, 0, 0);
            stageCamOffset = new Vector3(-6f, 6f, -20f);
        }

        else if (other.tag == "LADDER")
        {
            rapid = true;
            stageFocalOffset = new Vector3(0.5f, 0, 0);
            stageCamOffset = new Vector3(-15f, -1f, -30f);
            stageMin = -100f;
            stageMax = 30f;
        }

        else if (other.tag == "LADDER2")
        {
            rapid = true;
            stageFocalOffset = new Vector3(0.5f, 0, 0);
            stageCamOffset = new Vector3(-24f, -4f, -35f);
            stageMin = -100f;
            stageMax = 30f;
        }

        else if (other.tag == "LADDER3")
        {
            rapid = true;
            stageFocalOffset = new Vector3(0.5f, 0, 0);
            stageCamOffset = new Vector3(-6f, 6f, -20f);
            stageMin = -100f;
            stageMax = 30f;
        }
    }
}
