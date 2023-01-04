using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : PlayerMovement
{
    
    void Start()
    {        
        
    }
    void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0)&&!myAnim.GetBool("IsRolling")&&!myAnim.GetBool("IsAttacking"))
        {
            if(!myAnim.GetBool("IsKnife")) Weapon();
            else myAnim.SetTrigger("AttackA");
        }
        
    }
    public override void OnDamage(float dmg)
    {
        myStat.HP -= dmg;
        if (Mathf.Approximately(myStat.HP, 0.0f))
        {
            //»ç¸Á
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
    }
    

}
