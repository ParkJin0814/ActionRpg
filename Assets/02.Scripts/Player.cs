using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
                if (!Inventory.invectoryActivated)
                {
                    AttackA();                    
                }
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
    void Update()
    {
        
    }
    void AttackA()
    {
        if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsAttacking") )
        {
            if (myAnim.GetBool("IsKnife"))
            {
                Weapon();
                return;
            }
            else myAnim.SetTrigger("AttackA");
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
