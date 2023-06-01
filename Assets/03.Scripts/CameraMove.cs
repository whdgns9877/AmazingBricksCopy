using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform playerPos; //플레이어 위치

    [SerializeField] private Vector3 highestPos; //플레이어가 가장 높이 올라갔을때의 위치
    [SerializeField] private Vector3 pivotPos; //카메라움직임을 결정할 기준값
    [SerializeField] private Vector3 curPos; //현재 카메라의 위치

    // Start is called before the first frame update
    void Start()
    {
        //각각을 현재 카메라 위치로 초기화
        highestPos = transform.position;
        pivotPos = transform.position;
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position; //curPos에는 프레임 마다의 현재 카메라 위치를 담는다.
        //플레이어의 y포지션이 기준값y포지션을 넘어가면
        if (playerPos.position.y > pivotPos.y)
        {
            //카메라의 y좌표가 플레이어y좌표를 따라간다
            transform.position = new Vector2(0, playerPos.position.y);
        }

        if(curPos.y > highestPos.y) //현재 y포지션이 플레이어 최고 y위치를 넘어서면
        {
            pivotPos.y = highestPos.y = curPos.y; //현재 위치를 최고점으로 두고 기준 점도 그곳에 둔다
        }
    }
}
