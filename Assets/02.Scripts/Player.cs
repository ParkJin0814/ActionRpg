using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator myAnim;
    void Start()
    {
        myAnim= GetComponent<Animator>();
    }
        
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {

        }         
    }
}
