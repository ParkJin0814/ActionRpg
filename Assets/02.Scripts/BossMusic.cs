using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    
    void Start()
    {
        SoundManager.Inst.ResumeBGM();
    }
}
