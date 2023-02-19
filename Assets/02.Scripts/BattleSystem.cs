using UnityEngine;


public interface IBattle
{
    void OnDamage(float dmg);
    bool OnLive();
    
}

public class BattleSystem : CharacterProperty, IBattle
{
    public LayerMask myEnemyMask;
    public Transform[] AttackPoint;
    public float AttackPointRange;

    
    public virtual void OnDamage(float dmg)
    {
        
    }
    public virtual bool OnLive()
    {
        return true;
    }
    public void OnAttack(int a=0)
    {
        Collider[] list = Physics.OverlapSphere(AttackPoint[a].position, AttackPointRange, myEnemyMask);

        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(myDamage());
        }
    }
    public virtual float myDamage()
    {        
        switch(Random.Range(0, 10))
        {
            case 0:
                return myStat.AP * 1.2f;                
            default:
                break;
        }
        return myStat.AP;
    }

}
