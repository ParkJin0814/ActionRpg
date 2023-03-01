using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyCharacter : BattleSystem
{
    public string myName;
    public MonsterType myType;
    public Transform myHeadTop;
    public Transform myFloatingDamage;
    public GameObject floatingDamageText;
    public Transform myTarget;
    [SerializeField] float viewAngle;
    [SerializeField] float viewDistance;
    [SerializeField] GameObject hpbar;
    [SerializeField] float romingRage;
    [SerializeField] int myAttackLength = 1;
    Vector3 startPos;
    NavMeshAgent nav;
    GameObject obj;
    Coroutine moveCo = null;
    Coroutine attackCo = null;

    public enum MonsterType
    {
        Nomal, Boss
    }
    public enum STATE
    {
        Create, Idle, Roaming, Battle, LostTarget, Dead
    }
    public STATE myState = STATE.Create;

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (s)
        {
            case STATE.Create:
                break;
            case STATE.Idle:
                if (obj == null)
                {
                    if (myType == MonsterType.Nomal)
                    {
                        obj = Instantiate(hpbar, SceneData.Inst.Hpbar) as GameObject;
                        obj.GetComponent<HpBar>().myTarget = myHeadTop;
                        myStat.changeHp = (float v) => obj.GetComponent<HpBar>().myBar.value = v;
                    }
                    else if (myType == MonsterType.Boss)
                    {
                        obj = Instantiate(hpbar, SceneData.Inst.BossHpbar) as GameObject;
                        obj.GetComponent<BossHpBar>().myEC = this;
                    }
                    obj.gameObject.SetActive(false);
                }
                myAnim.SetBool("Walk", false);
                StartCoroutine(DelayRoaming(2.0f));
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-romingRage, romingRage);
                pos.z = Random.Range(-romingRage, romingRage);
                pos = startPos + pos;
                MoveToPosition(pos, () => ChangeState(STATE.Idle));
                break;
            case STATE.LostTarget:
                myTarget = null;
                obj.gameObject.SetActive(false);
                MoveToPosition(startPos, () => ChangeState(STATE.Idle));
                break;
            case STATE.Battle:
                if (obj != null)
                {
                    obj.gameObject.SetActive(true);
                }
                AttackTarget();
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                if (myName == GameManager.Inst.questManager.questMobName)
                {
                    switch (GameManager.Inst.questManager.questId)
                    {
                        case 20:
                            GameManager.Inst.questManager.QuestCountCheck();
                            break;
                        case 40:
                            GameObject dropItem = Instantiate(GameManager.Inst.GameObj[0], transform.position,
                                Quaternion.identity, SceneData.Inst.dropObject);
                            break;
                    }
                }

                StartCoroutine(DisApearing(2.0f, 4.0f));
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
                EnemyTarget();
                break;
            case STATE.Roaming:
                EnemyTarget();
                break;
            case STATE.LostTarget:
                break;
            case STATE.Battle:
                LostTarget();
                break;
            case STATE.Dead:
                break;
        }
    }
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        ChangeState(STATE.Idle);
        nav.speed = myStat.MoveSpeed;
    }

    private void FixedUpdate()
    {
        StateProcess();
        if (nav.remainingDistance.Equals(0.0f))
            myAnim.SetBool("Walk", false);
        else
            myAnim.SetBool("Walk", true);
    }
    void LostTarget()
    {
        if (MonsterType.Boss == myType) return;
        if (!myTarget.GetComponent<IBattle>().OnLive())
            ChangeState(STATE.LostTarget);
        if (myTarget != null && nav.remainingDistance != Mathf.Infinity && nav.remainingDistance > 10.0f)
            ChangeState(STATE.LostTarget);
    }
    public void EnemyTarget()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, myEnemyMask);
        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if (_targetTf.name == "Player")
            {
                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);
                if (_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if (_hit.transform.name == "Player" && _hit.transform.GetComponent<IBattle>().OnLive())
                        {
                            myTarget = _hit.transform;
                        }
                    }
                }
            }
        }
        if (myTarget != null)
        {
            ChangeState(STATE.Battle);
        }
    }
    public override void OnDamage(float dmg)
    {
        if (myState == STATE.Dead) return;
        myStat.HP -= dmg;
        OnFloatingDamage((int)dmg);
        if (myType == MonsterType.Boss && obj != null)
        {
            obj.SetActive(true);
            obj.GetComponent<BossHpBar>().HpbarValue();
        }
        if (Mathf.Approximately(myStat.HP, 0.0f))
            ChangeState(STATE.Dead);
        else
        {
            myAnim.SetTrigger("Damage");
            if (myTarget == null)
            {
                Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, myEnemyMask);
                foreach (Collider collider in _target)
                    if (collider.tag == "Player")
                        myTarget = collider.transform;
            }
        }
    }
    void OnFloatingDamage(int dmg, bool v = false)
    {
        GameObject obj = Instantiate(floatingDamageText, SceneData.Inst.FloatingDamage);
        obj.GetComponent<DamageText>().damage = dmg;
        obj.GetComponent<DamageText>().myTarget = myFloatingDamage;
    }
    IEnumerator DelayRoaming(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(STATE.Roaming);
    }
    protected void MoveToPosition(Vector3 pos, UnityAction done = null, bool Rot = true)
    {
        if (attackCo != null)
        {
            StopCoroutine(attackCo);
            attackCo = null;
        }
        if (moveCo != null)
        {
            StopCoroutine(moveCo);
            moveCo = null;
        }

        moveCo = StartCoroutine(MovingToPostion(pos, done));
    }
    IEnumerator MovingToPostion(Vector3 pos, UnityAction done)
    {
        nav.ResetPath();
        nav.SetDestination(pos);
        yield return new WaitForSeconds(0.3f);
        while (nav.remainingDistance != Mathf.Infinity && nav.remainingDistance > 0.0f)
        {
            yield return null;
        }
        done?.Invoke();
    }
    protected void AttackTarget()
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttckingTarget(myStat.AttackRange, myStat.AttackDelay));
    }
    IEnumerator AttckingTarget(float AttackRange, float AttackDelay)
    {
        float playTime = 0.0f;
        nav.ResetPath();
        while (myTarget != null)
        {
            if (!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;
            //이동
            Vector3 dir = myTarget.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();
            if (dist > AttackRange && !myAnim.GetBool("IsAttacking"))
                nav.SetDestination(myTarget.position);
            else
            {
                nav.ResetPath();
                myAnim.SetBool("Walk", false);
                if (playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    OnAttackLength();
                }
                float Angle = Vector3.Angle(transform.forward, dir);
                float rotDir = 1.0f;
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                    rotDir = -rotDir;
                if (Angle > 10f && nav.remainingDistance.Equals(0.0f))
                {
                    if (!myAnim.GetBool("IsAttacking"))
                    {
                        float delta = 180.0f * Time.deltaTime;
                        if (delta > Angle)
                            delta = Angle;
                        Angle -= delta;
                        transform.Rotate(Vector3.up * rotDir * delta, Space.World);
                    }
                }
            }
            yield return new WaitForFixedUpdate();
        }
        myAnim.SetBool("Walk", false);
    }
    IEnumerator DisApearing(float d, float t)
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(t);
        Destroy(obj.gameObject);
        float dist = d;
        while (dist > 0.0f)
        {
            float delta = 0.5f * Time.deltaTime;
            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }
    public override bool OnLive()
    {
        if (myState != STATE.Dead) return true;
        return false;
    }
    public void OnAttackLength()
    {
        int a = Random.Range(0, myAttackLength);
        myAnim.SetTrigger($"Attack{a}");
    }

}
