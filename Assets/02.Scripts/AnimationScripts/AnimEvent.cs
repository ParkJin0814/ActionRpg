using System.Collections;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public GameObject[] Effect;
    public Transform[] EffectPostion;
    public AudioClip EffectSound;

    public void OnEffect(int a)
    {
        GameObject obj = Instantiate(Effect[a]);
        obj.transform.position = EffectPostion[a].transform.position;
        if (EffectPostion[a].GetComponent<AudioSource>() != null)
        {
            SoundManager.Inst.PlayOneShot(EffectPostion[a].GetComponent<AudioSource>(), EffectSound);
        }
        StartCoroutine(DestroyObject(obj));
    }

    IEnumerator DestroyObject(GameObject obj, float t = 10.0f)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
