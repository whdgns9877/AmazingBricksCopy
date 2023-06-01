using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //��ֹ� ���� ������ enum�� �������� �÷����� ������, ������ ������ ����
    enum ObstacleColor { Blue, Red, Green, Cyan}
    [SerializeField] GameObject platformPrefab;
    [SerializeField] private int createCnt = 4;

    //������ �÷����� ���� ���ӿ�����Ʈ�� �迭�� �÷����� ���� �ٲ��ٶ� �ʿ��� ������
    private GameObject[] platforms;
    private int curIdx;
    private float curIdxYPos;
    private int changeColorCnt;
    private int changeColorIdx;

    //�ʱ� ���ۻ��´� �Ķ�������
    ObstacleColor obstacleColor = ObstacleColor.Blue;

    //���� ��ġ
    private Vector2 poolPos = new Vector2(25, 0);

    void Start()
    {
        //���� �������� �ʱ�ȭ
        curIdx = 0;
        changeColorIdx = 0;
        curIdxYPos = -19f; //-19�� ������ �÷��̾� ��ġ�� �°� ���������� �� ���� ���� ��������
        changeColorCnt = 0;
        platforms = new GameObject[createCnt];

        //���� �ε����� 0���� �����Ͽ� �� 4���� �����ϴµ�
        for(int curIdx = 0; curIdx < platforms.Length; curIdx++)
        {
            //�÷������� �����ϰ�
            platforms[curIdx] = Instantiate(platformPrefab, poolPos, Quaternion.identity);
            //��ġ���� �ε����� ���Ҷ� ���� curIdxYPos��� ������ �����ϰ� �̿� 13.5�� ������
            //�׾Ƽ� ��ġ��Ų��.
            platforms[curIdx].transform.position = new Vector2(0, curIdxYPos += 13.5f);
        }
    }

    
    void Update()
    {
        //���� ���� ���¸� �������� �ʴ´�.
        if (GameManager.instance.isGameover) return;

        //������ ������ ������ ����
        switch (changeColorIdx)
        {
            //�÷��� ���� ������ ������ �ٲ��ش�.
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

        //�׸��� �̷��� ���� ������ ����
        switch (obstacleColor)
        {
            //SpriteRenderer������Ʈ�� �̿��Ͽ� ���� �ٲ��ش�.
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

        //������ 10�� �ٲ�� �� 10���� ���������� 
        if (changeColorCnt == 10)
        {
            //Ƚ���� �ʱ�ȭ ���ش� 0~10 �ݺ� 
            changeColorCnt = 0;
            changeColorIdx++; //10���ݺ��Ҷ����� �ε����� �÷��� 
            if (changeColorIdx > 3) changeColorIdx = 0; //�� �ε����� 3�� �Ѿ�� �̰� ���� �ʱ�ȭ
            //�̷����ϸ� ���� 10������������ ���� �ٲ�� �� ���� �ٲ�°� 4���� �Ǹ� �ٽ� ���� ������
        }

        //���� �ε����� �÷����� ��Ȱ��ȭ �Ǹ�(�� �÷��̾ ������� ������ �Ѿ��)
        //�� �÷����� ���̻� ���� �������� ��Ȱ��ȭ �ϰ�
        if (platforms[curIdx].activeSelf == false)
        {
            //OnEnable�Լ��� �̿��ϱ����� �ٽ� Ȱ��ȭ ��Ų��
            platforms[curIdx].SetActive(true);
            //��ġ�� ������ ��ġ ������ curDixYPos ���� 15 �����Ͽ� ��ġ�Ѵ�.
            platforms[curIdx].transform.position = new Vector2(0, curIdxYPos += 15f);
            curIdx++; //�÷��� �ε��� �����ְ�
            changeColorCnt++; //�� �ٲ��� �������� �����ְ�

            //���� �ε����� �������� �Ѿ��
            if (curIdx >= createCnt)
            {
                curIdx = 0; //�ε��� �ʱ�ȭ 0~4�ݺ�
            }
        }
    }
}
