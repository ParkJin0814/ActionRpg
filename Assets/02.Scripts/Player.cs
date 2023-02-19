using UnityEngine;

public class Player : PlayerMovement
{
    public enum STATE
    {
        Create, Idle, Dead
    }
    public STATE myState = STATE.Create;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");                
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.Create:
                break;
            case STATE.Idle:                
                Movement();                
                break;
            case STATE.Dead:
                break;
        }
    }
    void Start()
    {
        Application.targetFrameRate= 60;
        ChangeState(STATE.Idle);
        ChangeStat();
        InputNumber.myPlayer = this;        
    }
    void ChangeStat()
    {
        myStat.changeHp = (float a) =>
        {
            GameManager.Inst.myHp.value = a;
        };
        myStat.changeSp = (float a) =>
        {
            GameManager.Inst.mySp.value = a;
        };
    }
    private void FixedUpdate()
    {
        StateProcess();
    }
    
    public void AttackA()
    {
        if (myState==STATE.Idle&&!myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsAttacking") )
        {
             myAnim.SetTrigger("AttackA");
        }        
    }
    public void RollinButton()
    {
        if (myState == STATE.Idle && !myAnim.GetBool("IsRolling") &&
            !myAnim.GetBool("IsRolling") && myStat.SP >= 20.0f && myStat.RecoverySp)
        {
            myAnim.SetTrigger("Rolling");
        }
    }
    public override void OnDamage(float dmg)
    {
         
        myStat.HP -= dmg;
        if (Mathf.Approximately(myStat.HP, 0.0f))
        {            
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }
    public override bool OnLive()
    {
        if (myState != STATE.Dead) return true;
        return false;
    }   

}
