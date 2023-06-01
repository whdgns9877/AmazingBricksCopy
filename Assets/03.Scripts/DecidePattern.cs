using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecidePattern : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;

    private void OnEnable()
    {
        int rand = Random.Range(0, 9); //0 ~ 8 ������ ���� ������ �����ϰ�
        // obstacles �迭�� �ε��� 8�������˻��Ͽ�
        for (int i = 0; i < obstacles.Length - 2; i++)
        {
            if(i == rand) //�˻��ϴ� �ε����� �������� ������
            {
                //�ش� �ε������� ���ӵ� ������ ������Ʈ�� ��Ȱ��ȭ ��Ų��
                obstacles[i].SetActive(false);
                obstacles[i + 1].SetActive(false);
                obstacles[i + 2].SetActive(false);
                break;
            }
        }
    }

    //������Ʈ�� ��Ȱ��ȭ �ɶ� ����
    private void OnDisable()
    {
        //���Ͽ� ����ϴ� ������Ʈ���� ���� Ȱ��ȭ ��Ų��
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(true);
        }
    }
}
