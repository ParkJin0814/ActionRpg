using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : BattleSystem
{
    //�̵�ó��
    float Run;
    float x;
    float y;
    bool runing = false;
    Coroutine myRoll;
    //������ ���ó��
    [SerializeField] float itemRange; //������ ����Ÿ�
    bool pickupItme; //������ ���氡�ɽ� �Ұ�
    private RaycastHit hitInfo;  // �浹ü ���� ����
    [SerializeField] LayerMask ItemLayerMask;
    [SerializeField] TMP_Text dropText;    
      
    protected void RollingStart()
    {
        if (myRoll == null)
        {
            myRoll = StartCoroutine(Rollings());
        }
    }
    protected void RollingEnd()
    {
        if (myRoll != null)
        {
            StopCoroutine(myRoll);
            myRoll = null;
        }
    }
    protected void Movement()
    {
        if (!myAnim.GetBool("IsRolling"))
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            myAnim.SetFloat("x", x);
            myAnim.SetFloat("y", y);
            //������
            if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling"))
            {
                myAnim.SetTrigger("Rolling");
            }
            //����Ż����
            if (Input.GetKeyDown(KeyCode.X)) Weapon();
            //�̵�
            if (!x.Equals(0.0f) || !y.Equals(0.0f)) myAnim.SetBool("Move", true);            
            else myAnim.SetBool("Move", false);
            //�޸���
            if (Input.GetKey(KeyCode.LeftShift) && y > 0.0f)
            {
                Run = Mathf.Lerp(0, 1, Run + 0.2f);
                runing = true;
            }
            else
            {
                Run = Mathf.Lerp(0, 1, Run - 0.2f);
                runing = false;
            }
            myAnim.SetFloat("Run", Run);
            if (myAnim.GetBool("Move"))
            {
                if (myAnim.GetBool("IsMoving"))
                {
                    Vector3 pos = Vector3.forward;
                    pos.x = x;
                    pos.z = y;
                    float delta = runing ? myStat.MoveSpeed * 1.5f * Time.deltaTime : myStat.MoveSpeed * Time.deltaTime;
                    pos.Normalize();
                    transform.Translate(pos * delta);

                }
            }
        }
    }
    protected void Weapon()
    {
        if (myAnim.GetBool("KnifeToggle")) return;
        myAnim.SetTrigger("Knife");
        myAnim.SetBool("IsKnife", !myAnim.GetBool("IsKnife"));
    }
    IEnumerator Rollings()
    {

        Vector3 dir = new Vector3(x, 0, 1);
        while (true)
        {
            float delta = myStat.MoveSpeed * 3.0f * Time.deltaTime;
            transform.Translate(dir * delta);
            yield return null;
        }

    }

    //������ ȹ��ó��
    protected void DropItem()
    {
        TryAction();
        CheckItem();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.SphereCast(transform.position, 0.0f, transform.forward, out hitInfo, itemRange, ItemLayerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            ItemInfoDisappear();
    }

    private void ItemInfoAppear()
    {
        pickupItme = true;
        dropText.gameObject.SetActive(true);
        dropText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " Drop " + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear()
    {
        pickupItme = false;
        dropText.gameObject.SetActive(false);
    }
    private void CanPickUp()
    {
        if (pickupItme)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�.");  // �κ��丮 �ֱ�
                Destroy(hitInfo.transform.gameObject);                
                ItemInfoDisappear();
            }
        }
    }
}
