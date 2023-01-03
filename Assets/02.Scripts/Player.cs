using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : PlayerMovement
{
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }
    void Update()
    {
        Movement();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("a");
        }
    }
}
