using UnityEngine;

public class BossMusic : MonoBehaviour
{

    void Start()
    {
        SoundManager.Inst.ResumeBGM();
    }
}
