using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�÷��̾� ������Ʈ�� �پ��ִ� Rigidbody2D �� CircleCollider2D ������Ʈ
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
            //���ӸŴ������� �����ϴ� isGameover ������ true��� ���� ���� �ʴ´�.
            if (GameManager.instance.isGameover == true) return;

            //������ Ű �Է��϶��� ����� �������� Addforce
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //���������� �ӷ��� �ʱ�ȭ ���� ������ �����̺پ� ���ϴ´�� �������� �ʴ´�.
                playerRg.velocity = Vector2.zero;
                playerRg.AddForce(new Vector2(160f, 600f));
                SoundManager.Instance.sounds[1].Play();
            }
            //���� Ű �Է��϶��� �»��� �������� Addforce
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerRg.velocity = Vector2.zero;
                playerRg.AddForce(new Vector2(-160f, 600f));
                SoundManager.Instance.sounds[1].Play();
            }
        }
    }

    //�浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʈ���ŵ� ������Ʈ�� �±װ� Die ���
        if(collision.tag == "Die")
        {
            playerRg.velocity = Vector2.zero; //�ӵ� 0����
            playerCollider.enabled = false; //�÷��̾� �ݶ��̴� ��Ȱ��ȭ
            GameManager.instance.isGameover = true; //���ӸŴ����� ���� isGameover true��
            StartCoroutine(Rotate()); //�ڷ�ƾ ����(������Ʈ ȸ����Ű�� ����)
            Invoke("ActiveFalse", 1.5f); //1.5�� �ڿ� ActiveFalse ��� �Լ� ����

            //Ʈ���ŵ� ������Ʈ�� �̸��� DeadZone_Bottom �̶�� ���� ���� �����ش�
            //���۸��������ӿ��� �������� �׾����� ȿ���� ����Ѱ�
            if (collision.name == "DeadZone_Bottom") playerRg.AddForce(new Vector2(0,600f));

            SoundManager.Instance.sounds[2].Play();

            //���� �Ŵ������� ��������Ʈ�� ����س��� �Լ� ����
            GameManager.instance.bestScoreDelegate();
        }

        //Ʈ���ŵ� ������Ʈ�� �±װ� AddScore���
        if(collision.tag == "AddScore")
        {
            //���ӸŴ����� AddScore �Լ�����
            GameManager.instance.AddScore();
            //�ش� ������Ʈ�� �ݶ��̴��� ��Ȱ��ȭ�Ѵ�
            //������ �̹� ���� �ø� �ݶ��̴��� �Դٰ��� �ϸ� ������ ��� �����⶧��
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator Rotate()
    {
        //while���� �̿��Ͽ� �÷��̾ ȸ����Ų��
        while(true)
        {
            transform.Rotate(0, 0, Time.deltaTime * -300f);
            yield return null; //�� yield �� ������ ����Ƽ ������� ����..
        }
    }

    //Invoke �Լ��� �̿��� 1.5�ʵڿ�
    private void ActiveFalse()
    {
        //���� ������Ʈ�� ��Ȱ��ȭ ���ش�.
        //�̶� ��Ȱ��ȭ �Ǹ鼭 �ڷ�ƾ�� �ڵ����� ȣ�� ����ȴ�.
        gameObject.SetActive(false);
        //���ӸŴ����� OnPlayerDead�Լ� ����
        GameManager.instance.OnPlayerDead();
    }
}
