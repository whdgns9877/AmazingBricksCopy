using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecidePattern : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;

    private void OnEnable()
    {
        int rand = Random.Range(0, 9); //0 ~ 8 사이의 랜덤 정수를 생성하고
        // obstacles 배열의 인덱스 8번까지검색하여
        for (int i = 0; i < obstacles.Length - 2; i++)
        {
            if(i == rand) //검사하는 인덱스가 랜덤값과 같으면
            {
                //해당 인덱스포함 연속된 새개의 오브젝트를 비활성화 시킨다
                obstacles[i].SetActive(false);
                obstacles[i + 1].SetActive(false);
                obstacles[i + 2].SetActive(false);
                break;
            }
        }
    }

    //오브젝트가 비활성화 될때 실행
    private void OnDisable()
    {
        //패턴에 사용하는 오브젝트들을 전부 활성화 시킨다
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }
    }
}
