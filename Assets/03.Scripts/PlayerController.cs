using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어 오브젝트에 붙어있는 Rigidbody2D 와 CircleCollider2D 컴포넌트
    [SerializeField] Rigidbody2D playerRg;
    [SerializeField] CircleCollider2D playerCollider;

    private bool isStart;

    private void Start()
    {
        isStart = false;
    }

    void Update()
    {
        if (GameManager.instance.Generator.activeSelf == false
            && (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            GameManager.instance.Generator.SetActive(true);
            GameManager.instance.ExplainText.SetActive(false);
            GameManager.instance.ExPlainArrow.SetActive(false);
            isStart = true;
        }

        if (isStart == true)
        {
            playerRg.gravityScale = 2f;
            GetComponent<Rigidbody2D>().gravityScale = 2f;
            //게임매니저에서 관리하는 isGameover 변수가 true라면 연산 하지 않는다.
            if (GameManager.instance.isGameover == true) return;

            //오른쪽 키 입력일때는 우상향 방향으로 Addforce
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //누를때마다 속력을 초기화 하지 않으면 가속이붙어 원하는대로 움직이지 않는다.
                playerRg.velocity = Vector2.zero;
                playerRg.AddForce(new Vector2(160f, 600f));
                SoundManager.Instance.sounds[1].Play();
            }
            //왼쪽 키 입력일때는 좌상향 방향으로 Addforce
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerRg.velocity = Vector2.zero;
                playerRg.AddForce(new Vector2(-160f, 600f));
                SoundManager.Instance.sounds[1].Play();
            }
        }
    }

    //충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //트리거된 오브젝트의 태그가 Die 라면
        if(collision.tag == "Die")
        {
            playerRg.velocity = Vector2.zero; //속도 0으로
            playerCollider.enabled = false; //플레이어 콜라이더 비활성화
            GameManager.instance.isGameover = true; //게임매니저의 변수 isGameover true로
            StartCoroutine(Rotate()); //코루틴 실행(오브젝트 회전시키는 내용)
            Invoke("ActiveFalse", 1.5f); //1.5초 뒤에 ActiveFalse 라는 함수 실행

            //트리거된 오브젝트의 이름이 DeadZone_Bottom 이라면 위로 힘을 가해준다
            //슈퍼마리오게임에서 마리오가 죽었을때 효과랑 비슷한것
            if (collision.name == "DeadZone_Bottom") playerRg.AddForce(new Vector2(0,600f));

            SoundManager.Instance.sounds[2].Play();

            //게임 매니저에서 델리게이트로 등록해놓은 함수 실행
            GameManager.instance.bestScoreDelegate();
        }

        //트리거된 오브젝트의 태그가 AddScore라면
        if(collision.tag == "AddScore")
        {
            //게임매니저의 AddScore 함수실행
            GameManager.instance.AddScore();
            //해당 오브젝트의 콜라이더를 비활성화한다
            //이유는 이미 점수 올린 콜라이더를 왔다갔다 하면 점수가 계속 오르기때문
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Rotate()
    {
        //while문을 이용하여 플레이어를 회전시킨다
        while(true)
        {
            transform.Rotate(0, 0, Time.deltaTime * -300f);
            yield return null; //요 yield 가 없으면 유니티 멈출수도 있음..
        }
    }

    //Invoke 함수를 이용해 1.5초뒤에
    private void ActiveFalse()
    {
        //게임 오브젝트를 비활성화 해준다.
        //이때 비활성화 되면서 코루틴은 자동으로 호출 종료된다.
        gameObject.SetActive(false);
        //게임매니저의 OnPlayerDead함수 실행
        GameManager.instance.OnPlayerDead();
    }
}
