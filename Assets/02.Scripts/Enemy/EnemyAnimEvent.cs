using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : MonoBehaviour
{
    public EnemyCharacter enemyCharacter;
    public void OnAttack(int a)
    {
        Collider[] list = Physics.OverlapSphere(enemyCharacter.AttackPoint[a].position, enemyCharacter.AttackPointRange[a], enemyCharacter.myEnemyMask);
        foreach (Collider col in list)
            col.GetComponent<IBattle>()?.OnDamage(enemyCharacter.myDamage());
    }
}
