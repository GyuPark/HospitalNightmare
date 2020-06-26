using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 목표1 : Game이 시작하면, Player의 위치를 start point에 둔다.
// 필요속성 : Player, Startpoint

// 목표2 : GameOVer되면 화면이 천천히 Blackout 되고 Main으로 돌아간다
// 1. Main으로 돌아간다
// 2. BlackFade - Image - Color의 Alpha값이 천천히 255까지 차오른다
// 3. GameOver되면 Player가 죽거나 Game이 끝나면

public class GameManager : MonoBehaviour
{
    GameObject player;
    public Transform startPoint;
    public Image image;
    Color tempColor;
    float _alpha;
    bool isGameOver;

    public bool GameOver
    {
        get
        {
            return isGameOver;
        }
        set
        {
            isGameOver = value;
        }
    }

    //Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        player = GameObject.Find("Player");
        player.transform.position = startPoint.position;    
    }

    private void Start()
    {
        tempColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            FinishGame();
        }
    }

    void FinishGame()
    {
        // 2. BlackFade - Image - Color의 Alpha값이 천천히 차오른다
        _alpha += Time.deltaTime;
        tempColor.a = _alpha;
        image.color = tempColor;

        // 1. 충분히 어두워지면 Main으로 돌아간다
        if (image.color.a >= 1.2f)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
