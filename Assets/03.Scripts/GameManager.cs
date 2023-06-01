using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour
{
    public delegate void BestScore();
    public BestScore bestScoreDelegate;
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public GameObject platform; //장애물 게임오브젝트
    public GameObject ExplainText; //설명 UI텍스트
    public GameObject ExPlainArrow;
    public GameObject Generator; //장애물 생성기

    public GameObject GameOverUI; //게임 오버시 출력해줄 UI
    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public Text recordText; // 최고 점수를 나타낼 텍스트

    public int Score { get; set; } = 0; // 게임 점수

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        //게임 매니저 생성시 화면 크기 설정하고 30프레임으로
        Screen.SetResolution(450, 800, true, 30);
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }

        //사운드 매니저에서 배경음악을 켜준다.
        SoundManager.Instance.sounds[0].Play();

        //최고점 계산 프로세스를 델리게이트에 등록하고
        bestScoreDelegate = ScoreProcess;
        //실행한다.
        bestScoreDelegate();
        //최초 시작 장애물의 색은 푸른색
        platform.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.blue;
    }

    // 점수를 증가시키는 메서드
    public void AddScore()
    {
        //게임오버가 아니라면
        if (!isGameover)
        {
            //점수증가
            Score++;
            scoreText.text = Score.ToString();
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;  //게임 오버 변수 true로
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 활성화 되어있는 씬을 재시작
    }

    //최고점 계산 프로세스
    private void ScoreProcess()
    {
        //최고점을 BestScore 라는 키로 받아온다
        int bestScore = PlayerPrefs.GetInt("BestScore");

        //현재 점수가 최고점을 넘었으면
        if(Score > bestScore)
        {
            bestScore = Score; //갱신하여
            PlayerPrefs.SetInt("BestScore", bestScore); //BestScore 키로 저장한다
        }
        //텍스트 갱신
        recordText.text = "Best Score : " + bestScore;
    }

    //최고점을 리셋하는 함수를 두고 설명 스크린에 버튼을 하나두어 연결하여 사용
    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0); //BestScore 키에 0값 설정
        recordText.text = "Best Score : 0"; //텍스트 표시 또한 바꿔줌
    }
}