using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Movement()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
        if (Input.GetKeyDown(KeyCode.X)&& !myAnim.GetBool("KnifeToggle"))
        {
            myAnim.SetTrigger("Knife");
            myAnim.SetBool("IsKnife", !myAnim.GetBool("IsKnife"));
        }
        if (Input.GetKey(KeyCode.LeftShift)&&y>0.0f)
        {
            Run = Mathf.Lerp(0, 1, Run + 0.02f);
            runing=true;
        }
        else
        {
            Run = Mathf.Lerp(0, 1, Run - 0.02f);
            runing=false;
        }
        myAnim.SetFloat("Run", Run);
        Vector3 pos = transform.position;
        pos.x += x;
        pos.z += y;
        float delta = runing? PlayerSpeed *1.5f* Time.deltaTime : PlayerSpeed *  Time.deltaTime;
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        if(!myAnim.GetBool("KnifeToggle"))transform.Translate(dir * delta, Space.World);

    }
}
