using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyCharacter : BattleSystem
{
    [SerializeField] Transform myTarget;
    [SerializeField] float viewAngle;
    [SerializeField] float viewDistance;
    Coroutine moveCo = null;    
    Coroutine attackCo = null;
    Vector3 startPos;
    NavMeshAgent nav;
    public enum STATE
    {
        Create, Idle, Roaming, Battle, LostTarget,Dead
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
                myAnim.SetBool("Walk", false);
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
    }

    private void FixedUpdate()
    {
        StateProcess();
        if(nav.remainingDistance.Equals(0.0f))
        {
            myAnim.SetBool("Walk", false);
        }
        else
        {
            myAnim.SetBool("Walk", true);
        }
    }
    void Update()
    {
        
    }
    void LostTarget()
    {
        if (!myTarget.GetComponent<IBattle>().OnLive())
        {
            myTarget = null;
            ChangeState(STATE.LostTarget);            
        }
        if (myTarget != null)
        {            
            if (nav.remainingDistance!=Mathf.Infinity&&nav.remainingDistance > 10.0f)
            {
                myTarget = null;                
                ChangeState(STATE.LostTarget);
            }
        }
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
                        if (_hit.transform.name == "Player" &&_hit.transform.GetComponent<IBattle>().OnLive())
                        {
                            myTarget = _hit.transform;                            
                        }
                    }
                }
            }
        }
        if (myTarget!=null)
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
    }
    IEnumerator MovingToPostion(Vector3 pos, UnityAction done)
    {
        nav.ResetPath();
        nav.SetDestination(pos);        
        yield return new WaitForSeconds(3.0f);        
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
            if (dist > AttackRange)
            {                
                nav.SetDestination(myTarget.position);
            }
            else
            {
                nav.ResetPath();
                myAnim.SetBool("Walk", false);
                if (playTime >= AttackDelay)
                {
                    //공격
                    playTime = 0.0f;
                    myAnim.SetTrigger("Attack");
                }
                float Angle = Vector3.Angle(transform.forward, dir);
                float rotDir = 1.0f;
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    rotDir = -rotDir;
                }
                if (Angle > 10f && nav.remainingDistance.Equals(0.0f))
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
                }
            }               
            yield return new WaitForFixedUpdate();
        }
        myAnim.SetBool("Walk", false);
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
    public override bool OnLive()
    {
        if (myState != STATE.Dead) return true;
        return false;
    }
}
