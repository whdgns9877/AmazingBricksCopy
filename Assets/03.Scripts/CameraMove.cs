using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform playerPos; //�÷��̾� ��ġ

    [SerializeField] private Vector3 highestPos; //�÷��̾ ���� ���� �ö������� ��ġ
    [SerializeField] private Vector3 pivotPos; //ī�޶�������� ������ ���ذ�
    [SerializeField] private Vector3 curPos; //���� ī�޶��� ��ġ

    // Start is called before the first frame update
    void Start()
    {
        //������ ���� ī�޶� ��ġ�� �ʱ�ȭ
        highestPos = transform.position;
        pivotPos = transform.position;
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = transform.position; //curPos���� ������ ������ ���� ī�޶� ��ġ�� ��´�.
        //�÷��̾��� y�������� ���ذ�y�������� �Ѿ��
        if (playerPos.position.y > pivotPos.y)
        {
            //ī�޶��� y��ǥ�� �÷��̾�y��ǥ�� ���󰣴�
            transform.position = new Vector2(0, playerPos.position.y);
        }

        if(curPos.y > highestPos.y) //���� y�������� �÷��̾� �ְ� y��ġ�� �Ѿ��
        {
            pivotPos.y = highestPos.y = curPos.y; //���� ��ġ�� �ְ������� �ΰ� ���� ���� �װ��� �д�
        }
    }
}
