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
                SceneData.Inst.GameOver.SetActive(true);
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
        Application.targetFrameRate = 60;
        ChangeState(STATE.Idle);
        ChangeStat();
        InputNumber.myPlayer = this;
    }
    void ChangeStat()
    {
        myStat.changeHp = (float a) =>
        {
            SceneData.Inst.myHp.value = a;
        };
        myStat.changeSp = (float a) =>
        {
            SceneData.Inst.mySp.value = a;
        };
    }
    private void FixedUpdate()
    {
        StateProcess();
    }

    public void AttackA(int a)
    {
        if (myState == STATE.Idle && !myAnim.GetBool("IsRolling") && !myAnim.GetBool("IsAttacking"))
        {
            switch (a)
            {
                case 0:
                    myAnim.SetTrigger("AttackA");
                    break;
                case 1:
                    myAnim.SetTrigger("AttackB");
                    break;
            }

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
    
    public override bool OnLive()
    {
        if (myState != STATE.Dead) return true;
        return false;
    }
    public override void OnAttack(int a)
    {        
        int attackPostion = 0;
        int attackRange = 0;
        float attackDamage = 1.0f;
        switch (a)
        {
            case 0:
                attackPostion = a;
                break;
            case 1:
                attackPostion = a;
                break;
            case 2:
                attackRange = 1;
                attackPostion = a;
                attackDamage = 1.1f;
                break;
            case 3:
                attackRange = 2;
                attackPostion = 0;
                attackDamage = 1.5f;
                break;
        }
        Collider[] list = Physics.OverlapSphere
            (AttackPoint[attackPostion].position
            , AttackPointRange[attackRange]
            , myEnemyMask);
        foreach (Collider col in list)
        {
            col.GetComponent<IBattle>()?
                .OnDamage(myDamage() * attackDamage);
        }

    }
    public override void OnDamage(float dmg)
    {
        myStat.HP -= dmg;
        if (Mathf.Approximately(myStat.HP, 0.0f)) 
            ChangeState(STATE.Dead);
        else 
            myAnim.SetTrigger("Damage");
    }
    public void OnPotion()
    {
        SceneData.Inst.myInventory.UsePotion();
    }
    public override float myDamage()
    {
        float dmg = myStat.AP + SceneData.Inst.myEquipment.EquipmentSlotValue(Item.EquipmentType.Weapon);
        switch (Random.Range(0, 10))
        {
            case 0:
                return dmg * 1.2f;
            default:
                break;
        }
        return dmg;
    }
}
