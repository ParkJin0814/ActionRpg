using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    void OnDamage(float dmg);
    bool OnLive();
    
}

public class BattleSystem : CharacterProperty, IBattle
{
    public LayerMask myEnemyMask;
    public Transform LeftAttackPoint;
    public Transform RightAttackPoint;
    public virtual void OnDamage(float dmg)
    {
        
    }
    public virtual bool OnLive()
    {
        return true;
    }
    public void LeftAttackTarget()
    {
        Collider[] list = Physics.OverlapSphere(LeftAttackPoint.position, 0.2f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(30.0f);
        }
    }
    public void RightAttackTarget()
    {
        Collider[] list = Physics.OverlapSphere(RightAttackPoint.position, 0.2f, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(30.0f);
        }
    }

}
