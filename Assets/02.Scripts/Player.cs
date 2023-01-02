using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    Animator myAnim;
    float Run;
    float x;
    float y;
    bool runing = false;    
    void Start()
    {
        myAnim= GetComponent<Animator>();
    }
        
    void Update()
    {
        Movement();
        
    }
    void Rolling()
    {
        Vector3 pos = transform.position;
        pos = Vector3.back - pos;
        pos = (Vector3.right * x) - pos;
        StartCoroutine(myRolling(pos));
    }
    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
        if (Input.GetKeyDown(KeyCode.Space) && !myAnim.GetBool("IsRolling"))
        {
            myAnim.SetTrigger("Rolling");
        }
        if (Input.GetKeyDown(KeyCode.X) && !myAnim.GetBool("KnifeToggle"))
        {
            myAnim.SetTrigger("Knife");
            myAnim.SetBool("IsKnife", !myAnim.GetBool("IsKnife"));
        }
        if (!x.Equals(0.0f) || !y.Equals(0.0f))
        {
            myAnim.SetBool("Move", true);
        }
        else
        {
            myAnim.SetBool("Move", false);
        }
        if (Input.GetKey(KeyCode.LeftShift) && y > 0.0f)
        {
            Run = Mathf.Lerp(0, 1, Run + 0.2f);
            runing = true;
        }
        else
        {
            Run = Mathf.Lerp(0, 1, Run - 0.2f);
            runing = false;
        }
        myAnim.SetFloat("Run", Run);
        if (myAnim.GetBool("Move")&&!myAnim.GetBool("IsRolling"))
        {            
            if (myAnim.GetBool("IsMoving"))
            {   
                Vector3 pos=Vector3.forward;
                pos.x = x;
                pos.z = y;
                float delta = runing ? PlayerSpeed * 1.5f * Time.deltaTime : PlayerSpeed * Time.deltaTime;                
                pos.Normalize();
                transform.Translate(pos * delta);
                
            }
        }
    }
    IEnumerator myRolling(Vector3 pos)
    {        
                
        pos.Normalize();                
        while (myAnim.GetBool("IsRolling"))
        {
            float delta = PlayerSpeed * 2.5f * Time.deltaTime;            
            transform.Translate(pos * delta);
            yield return null;
        }

    }
}
