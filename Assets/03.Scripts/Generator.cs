using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //장애물 색을 결정할 enum과 생성해줄 플랫폼의 프리팹, 생성할 프리팹 개수
    enum ObstacleColor { Blue, Red, Green, Cyan}
    [SerializeField] GameObject platformPrefab;
    [SerializeField] private int createCnt = 4;

    //생성한 플랫폼을 담을 게임오브젝트형 배열과 플랫폼의 색을 바꿔줄때 필요한 변수들
    private GameObject[] platforms;
    private int curIdx;
    private float curIdxYPos;
    private int changeColorCnt;
    private int changeColorIdx;

    //초기 시작상태는 파란색으로
    ObstacleColor obstacleColor = ObstacleColor.Blue;

    //생성 위치
    private Vector2 poolPos = new Vector2(25, 0);

    void Start()
    {
        //각종 변수들을 초기화
        curIdx = 0;
        changeColorIdx = 0;
        curIdxYPos = -19f; //-19인 이유는 플레이어 위치에 맞게 설정했을때 저 값이 가장 괜찮았음
        changeColorCnt = 0;
        platforms = new GameObject[createCnt];

        //현재 인덱스는 0부터 시작하여 총 4개를 생성하는데
        for(int curIdx = 0; curIdx < platforms.Length; curIdx++)
        {
            //플랫폼들을 생성하고
            platforms[curIdx] = Instantiate(platformPrefab, poolPos, Quaternion.identity);
            //위치들을 인덱스가 변할때 마다 curIdxYPos라는 변수에 저장하고 이에 13.5씩 높여서
            //쌓아서 위치시킨다.
            platforms[curIdx].transform.position = new Vector2(0, curIdxYPos += 13.5f);
        }
    }

    
    void Update()
    {
        //게임 오버 상태면 연산하지 않는다.
        if (GameManager.instance.isGameover) return;

        //위에서 설정한 변수에 따라
        switch (changeColorIdx)
        {
            //플랫폼 색을 결정할 변수를 바꿔준다.
            case 0:
                obstacleColor = ObstacleColor.Blue;
                break;

            case 1:
                obstacleColor = ObstacleColor.Red;
                break;

            case 2:
                obstacleColor = ObstacleColor.Green;
                break;

            case 3:
                obstacleColor = ObstacleColor.Cyan;
                break;
        }

        //그리고 이렇게 나온 변수에 따라
        switch (obstacleColor)
        {
            //SpriteRenderer컴포넌트를 이용하여 색을 바꿔준다.
            case ObstacleColor.Blue:
                platformPrefab.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.blue;
                break;

            case ObstacleColor.Red:
                platformPrefab.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.red;
                break;

            case ObstacleColor.Green:
                platformPrefab.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.green;
                break;

            case ObstacleColor.Cyan:
                platformPrefab.GetComponentInChildren<SpriteRenderer>().sharedMaterial.color = Color.cyan;
                break;
        }

        //점수가 10번 바뀌면 즉 10점을 먹을때마다 
        if (changeColorCnt == 10)
        {
            //횟수는 초기화 해준다 0~10 반복 
            changeColorCnt = 0;
            changeColorIdx++; //10번반복할때마다 인덱스는 올려서 
            if (changeColorIdx > 3) changeColorIdx = 0; //이 인덱스가 3을 넘어가면 이것 또한 초기화
            //이렇게하면 점수 10점먹을때마다 색이 바뀌고 이 색이 바뀌는게 4번이 되면 다시 원래 색으로
        }

        //현재 인덱스의 플랫폼이 비활성화 되면(즉 플레이어가 어느정도 지점을 넘어가서)
        //이 플랫폼이 더이상 쓸모 없어질때 비활성화 하고
        if (platforms[curIdx].activeSelf == false)
        {
            //OnEnable함수를 이용하기위해 다시 활성화 시킨후
            platforms[curIdx].SetActive(true);
            //위치를 마지막 배치 시점인 curDixYPos 보다 15 높게하여 배치한다.
            platforms[curIdx].transform.position = new Vector2(0, curIdxYPos += 15f);
            curIdx++; //플랫폼 인덱스 높여주고
            changeColorCnt++; //색 바꿔줄 변수또한 높여주고

            //현재 인덱스가 생성수를 넘어가면
            if (curIdx >= createCnt)
            {
                curIdx = 0; //인덱스 초기화 0~4반복
            }
        }
    }
}
