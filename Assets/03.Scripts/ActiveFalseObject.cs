using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalseObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //트리거 처리된 오브젝트의 태그가 Die 이고 해당 오브젝트의 이름이 DeadZone_Bottom 이라면
        if (collision.gameObject.tag == "Die" && collision.gameObject.name == "DeadZone_Bottom")
        {
            gameObject.SetActive(false); //플랫폼 비활성화
            //GetChild의 해당 인덱스가 플레이어 점수 올려주는 콜라이더인데
            //플레이어가 점수를 먹고 해당 콜라이더를 꺼주기 때문에
            //이를 재배치 할때 콜라이더가 켜진상태로 재배치해줘야 하기때문에 활성화
            transform.GetChild(5).GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
