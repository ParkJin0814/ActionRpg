using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeToggle : MonoBehaviour
{
    [SerializeField] Transform LeftKnife;
    [SerializeField] Transform RightKnife;
    [SerializeField] Transform LeftHand;
    [SerializeField] Transform RightHand;
    [SerializeField] Transform LeftHolder;
    [SerializeField] Transform RightHolder;
    public bool isKnife = false;

    void Start()
    {
        isKnife = GetComponent<Animator>().GetBool("IsKnife");
    }

    void TakeUpKnife()
    {
        isKnife = true;
        RightKnife.SetParent(RightHand, false);
        LeftKnife.SetParent(LeftHand, false);
    }
    void HolderKnife()
    {
        isKnife = false;
        RightKnife.SetParent(RightHolder, false);
        LeftKnife.SetParent(LeftHolder, false);
    }   
}
