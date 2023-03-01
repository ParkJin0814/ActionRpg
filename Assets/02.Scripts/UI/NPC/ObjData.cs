using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isNPC;
    public AudioClip hi;
    public AudioSource mySource;

    public void OnTalk()
    {
        if (hi != null)
            SoundManager.Inst.PlayOneShot(mySource, hi);
    }
}
