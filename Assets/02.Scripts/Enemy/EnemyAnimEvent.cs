using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : CharacterProperty
{
    public GameObject[] Effect;
    public Transform[] EffectPostion;
    public EnemyCharacter enemyCharacter;
    public void OnAttack(int a)
    {
        Collider[] list = Physics.OverlapSphere(enemyCharacter.AttackPoint[a].position, enemyCharacter.AttackPointRange[a], enemyCharacter.myEnemyMask);
        foreach (Collider col in list)
            col.GetComponent<IBattle>()?.OnDamage(enemyCharacter.myDamage());
    }    

    public void OnEffect(int a)
    {
        if (!myAnim.GetBool("isExplosion"))
        {
            GameObject obj = Instantiate(Effect[a]);
            obj.transform.position = EffectPostion[a].transform.position;
            StartCoroutine(DestroyObject(obj));
        }
    }

    IEnumerator DestroyObject(GameObject obj, float t = 3.8f)
    {
        myAnim.SetBool("isExplosion", true);
        yield return new WaitForSeconds(t);
        bool v=true;
        float time = 0.0f;
        while(v)
        {
            time+= Time.deltaTime;
            Collider[] list = Physics.OverlapSphere(obj.transform.position,3.0f , enemyCharacter.myEnemyMask);            
            foreach (Collider col in list)
            {
                if (col.GetComponent<IBattle>() != null)
                {
                    col.GetComponent<IBattle>().OnDamage(30.0f);
                    v= false;
                    break;
                }
            }
            if(time> 2.0f)
            {
                Destroy(obj);
                myAnim.SetBool("isExplosion", false);                
                yield break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(2.0f-time);
        myAnim.SetBool("isExplosion", false);        
        Destroy(obj);
    }
}
