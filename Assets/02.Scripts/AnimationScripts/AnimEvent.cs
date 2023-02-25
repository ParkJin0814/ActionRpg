using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public GameObject[] Effect;
    public Transform[] EffectPostion;

    public void OnEffect(int a)
    {
        GameObject obj = Instantiate(Effect[a]);
        obj.transform.position = EffectPostion[a].transform.position;
    }

}
