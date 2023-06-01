using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePos : MonoBehaviour
{
    //���� ��ġ ���� ����
    private float generatePosX;
    private float generatePosY;

    void OnEnable()
    {
        generatePosX = Random.Range(0, 10.4f) - 5.2f; // -5.2 ~ 5.2 ������ ������

        //���� ��� ��ֹ��̸� 23
        if (this.name == "Obstacle_up") generatePosY = 23f;
        else generatePosY = 17f; //�Ʒ����̸� 17

        //Platform �������� �ڽ����� ���ӵǾ��ֱ⶧���� localPosition ���
        transform.localPosition = new Vector2(generatePosX, generatePosY);
    }
}
