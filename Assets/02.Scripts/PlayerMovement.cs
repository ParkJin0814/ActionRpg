using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed;
    protected Animator myAnim;
    float Run;
    float x;
    float y;
    bool runing = false;
    Coroutine myRoll;
      
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
            if (Input.GetKeyDown(KeyCode.X) && !myAnim.GetBool("KnifeToggle"))
            {
                myAnim.SetTrigger("Knife");
                myAnim.SetBool("IsKnife", !myAnim.GetBool("IsKnife"));
            }

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
                    float delta = runing ? PlayerSpeed * 1.5f * Time.deltaTime : PlayerSpeed * Time.deltaTime;
                    pos.Normalize();
                    transform.Translate(pos * delta);

                }
            }
        }
    }
    IEnumerator Rollings()
    {

        Vector3 dir = new Vector3(x, 0, 1);
        while (true)
        {
            float delta = PlayerSpeed * 3.0f * Time.deltaTime;
            transform.Translate(dir * delta);
            yield return null;
        }

    }
}