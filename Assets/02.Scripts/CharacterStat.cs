using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public struct CharacterStat
{
    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] float sp;
    [SerializeField] float maxSp;
    [SerializeField] float moveSpeed;
    [SerializeField] float ap;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;

    public bool RecoverySp;
    public float spCool;
    public UnityAction<float> changeHp;
    public UnityAction<float> changeSp;
    public float HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHp);
            changeHp?.Invoke(hp / maxHp);
        }
    }
    
    public float SP
    {
        get => sp;
        set
        {
            sp = Mathf.Clamp(value, 0.0f, maxSp);
            if(sp.Equals(0.0f))
            {
                spCool= 2.0f;
                RecoverySp = false;
            }
            if(sp==maxSp)
            {
                RecoverySp = true;
            }
            changeSp?.Invoke(sp / maxSp);
        }
    }
    public float MoveSpeed
    {
        get => moveSpeed;
    }    
    public float AP
    {
        get => ap;
    }
    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackDelay
    {
        get => attackDelay;
    }
}
