using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : BattleSystem
{
    //이동처리
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
            //구르기
            if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling"))
            {
                myAnim.SetTrigger("Rolling");
            }
            //무기탈부착
            if (Input.GetKeyDown(KeyCode.X)) Weapon();
            //이동
            if (!x.Equals(0.0f) || !y.Equals(0.0f)) myAnim.SetBool("Move", true);            
            else myAnim.SetBool("Move", false);
            //달리기
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
    
}
