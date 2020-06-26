using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{



    [System.Serializable]
    public class Sound
    {
        // 음악 이름 설정
        public string soundName;
        // 오디오 클립 선언
        public AudioClip clip;
    }

    // 사운드를 등록, 여러가지라 배열로 선언
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSound;

    // 오디오 소스 선언
    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    // Start is called before the first frame update
    void Start()
    {
        PlayRandomBGM();        
    }

    void Update()
    {

    }

    // 보스 트리거 안에 들어가면 음악을 바꾸고 싶다, 빠져나오면 원래대로
    public void PlayRandomBGM()
    {
        // bgm 1번을 선택해라
        bgmPlayer.clip = bgmSound[0].clip;
        // 플레이 해라
        bgmPlayer.Play();
    }
}
