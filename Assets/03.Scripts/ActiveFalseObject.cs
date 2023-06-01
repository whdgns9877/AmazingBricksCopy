using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalseObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ʈ���� ó���� ������Ʈ�� �±װ� Die �̰� �ش� ������Ʈ�� �̸��� DeadZone_Bottom �̶��
        if (collision.gameObject.tag == "Die" && collision.gameObject.name == "DeadZone_Bottom")
        {
            gameObject.SetActive(false); //�÷��� ��Ȱ��ȭ
            //GetChild�� �ش� �ε����� �÷��̾� ���� �÷��ִ� �ݶ��̴��ε�
            //�÷��̾ ������ �԰� �ش� �ݶ��̴��� ���ֱ� ������
            //�̸� ���ġ �Ҷ� �ݶ��̴��� �������·� ���ġ����� �ϱ⶧���� Ȱ��ȭ
            transform.GetChild(5).GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
