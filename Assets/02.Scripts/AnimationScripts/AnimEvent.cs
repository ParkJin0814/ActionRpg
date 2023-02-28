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
        StartCoroutine(DestroyObject(obj));
    }

    IEnumerator DestroyObject(GameObject obj, float t = 10.0f)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
