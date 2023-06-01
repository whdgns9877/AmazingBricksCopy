using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePos : MonoBehaviour
{
    //생성 위치 담을 변수
    private float generatePosX;
    private float generatePosY;

    void OnEnable()
    {
        generatePosX = Random.Range(0, 10.4f) - 5.2f; // -5.2 ~ 5.2 사이의 랜덤값

        //위쪽 담당 장애물이면 23
        if (this.name == "Obstacle_up") generatePosY = 23f;
        else generatePosY = 17f; //아래쪽이면 17

        //Platform 프리팹의 자식으로 종속되어있기때문에 localPosition 사용
        transform.localPosition = new Vector2(generatePosX, generatePosY);
    }
}
