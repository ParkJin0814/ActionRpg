using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class EnemyCharacter : BattleSystem
{
    Transform myTarget;
    Coroutine moveCo = null;
    Coroutine rotCo = null;
    Coroutine attackCo = null;
    Vector3 startPos = Vector3.zero;
    public enum STATE
    {
        Create, Idle, Roaming, Battle, LostTarget,Dead
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
                StartCoroutine(DelayRoaming(2.0f));
                break;
            case STATE.Roaming:
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-3.0f, 3.0f);
                pos.z = Random.Range(-3.0f, 3.0f);
                pos = startPos + pos;
                MoveToPosition(pos, () => ChangeState(STATE.Idle));
                break;
            case STATE.LostTarget:                
                MoveToPosition(startPos, () => ChangeState(STATE.Idle));
                break;
            case STATE.Battle:
                AttackTarget();
                break;
            case STATE.Dead:
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");                
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
                EnemyTarget();
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
        startPos = transform.position;
        ChangeState(STATE.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }
    void LostTarget()
    {
        if (myTarget != null)
        {
            Vector3 pos = myTarget.position - transform.position;
            float dist = pos.magnitude;
            if (dist > 5.0f)
            {
                myTarget = null;
                ChangeState(STATE.LostTarget);
            }
        }
    }
    public void EnemyTarget()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 5.0f, myEnemyMask);        
        foreach (Collider col in list)
        {
            myTarget=col.GetComponent<Transform>();
        }
        if (myTarget != null)
        {
            ChangeState(STATE.Battle);            
        }        
    }
    public override void OnDamage(float dmg)
    {
        myStat.HP -= dmg;        
        if (Mathf.Approximately(myStat.HP, 0.0f))
        {
            //사망
            ChangeState(STATE.Dead);
        }
        else
        {
            myAnim.SetTrigger("Damage");
        }
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

        if (Rot)
        {
            if (rotCo != null)
            {
                StopCoroutine(rotCo);
                rotCo = null;
            }
            rotCo = StartCoroutine(RotatingToPosition(pos));
        }
    }
    IEnumerator MovingToPostion(Vector3 pos, UnityAction done)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        //달리기 시작
        myAnim.SetBool("Run", false);
        myAnim.SetBool("Walk", true);
        while (dist > 0.0f)
        {

            if (myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool("Walk", false);
                yield break;
            }


            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = myStat.MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        //달리기 끝 - 도착
        myAnim.SetBool("Walk", false);
        done?.Invoke();
    }
    IEnumerator RotatingToPosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float Angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -rotDir;
        }

        while (Angle > 0.0f)
        {
            if (!myAnim.GetBool("IsAttacking"))
            {
                float delta = 180.0f * Time.deltaTime;
                if (delta > Angle)
                {
                    delta = Angle;
                }
                Angle -= delta;
                transform.Rotate(Vector3.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
    protected void AttackTarget()
    {
        StopAllCoroutines();
        attackCo = StartCoroutine(AttckingTarget(myStat.AttackRange, myStat.AttackDelay));
    }
    IEnumerator AttckingTarget( float AttackRange, float AttackDelay)
    {
        float playTime = 0.0f;
        float delta = 0.0f;
        while (myTarget != null)
        {
            if (!myAnim.GetBool("IsAttacking")) playTime += Time.deltaTime;
            //이동
            Vector3 dir = myTarget.position - transform.position;
            float dist = dir.magnitude;
            dir.Normalize();
            if (dist > AttackRange)
            {
                myAnim.SetBool("Walk", false);
                myAnim.SetBool("Run", true);
                delta = myStat.MoveSpeed*1.5f * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);
            }
            else
            {
                myAnim.SetBool("Run", false);
                if (playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    myAnim.SetTrigger("AttackA");
                }
            }
            //회전
            delta = 180.0f * Time.deltaTime;
            float Angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(transform.right, dir) < 0.0f)
            {
                rotDir = -rotDir;
            }
            if (delta > Angle)
            {
                delta = Angle;
            }
            transform.Rotate(Vector3.up * delta * rotDir, Space.World);

            yield return null;
        }
        myAnim.SetBool("Run", false);
    }
    IEnumerator DisApearing(float d, float t)
    {
        yield return new WaitForSeconds(t);        
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
}
