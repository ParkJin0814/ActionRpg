using System.Collections;
using UnityEngine;

public class PlayerMovement : BattleSystem
{
    //이동처리
    float Run;
    float x;
    float y;
    bool runing = false;
    Coroutine myRoll;
    [SerializeField] VariableJoystick joy;
    bool runButton = false;

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
            x = joy.Horizontal;
            y = joy.Vertical;
            myAnim.SetFloat("x", x);
            myAnim.SetFloat("y", y);
            //이동
            if (!x.Equals(0.0f) || !y.Equals(0.0f)) myAnim.SetBool("Move", true);
            else myAnim.SetBool("Move", false);
            //달리기
            if (runButton && y > 0.0f && myStat.SP > 0.0f && myStat.RecoverySp)
            {
                Run = Mathf.Lerp(0, 1, Run + 0.2f);
                myStat.SP -= Time.deltaTime * 10.0f;
                runing = true;
            }
            else
            {
                Run = Mathf.Lerp(0, 1, Run - 0.2f);
                runing = false;
                if (myStat.spCool > 0.0f)
                {
                    myStat.spCool -= Time.deltaTime;
                    if (myStat.spCool < 0.0f) myStat.spCool = 0.0f;
                }
                else myStat.SP += Time.deltaTime * 20.0f;
            }
            myAnim.SetFloat("Run", Run);
            if (myAnim.GetBool("Move"))
            {
                if (myAnim.GetBool("IsMoving"))
                {
                    Vector3 pos = Vector3.forward;
                    pos.x = x;
                    pos.z = y;
                    float delta = runing ? myStat.MoveSpeed * 1.5f * Time.deltaTime 
                                         : myStat.MoveSpeed * Time.deltaTime;
                    pos.Normalize();
                    transform.Translate(pos * delta);

                }
            }
        }
    }


    IEnumerator Rollings()
    {
        Vector3 dir = new Vector3(x, 0, 1);
        myStat.SP -= 20.0f;
        while (true)
        {

            float delta = myStat.MoveSpeed * 3.0f * Time.deltaTime;
            transform.Translate(dir * delta);
            yield return new WaitForFixedUpdate();
        }
    }
    public void RunButtonDown()
    {
        runButton = true;
    }
    public void RunButtonUp()
    {
        runButton = false;
    }

}
