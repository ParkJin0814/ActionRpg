using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    public EnemyCharacter myEC;
    [SerializeField] Slider[] Hpbars;
    [SerializeField] TMP_Text myName;
    int hpbarLength;
    private void Start()
    {
        Hpbars = GetComponentsInChildren<Slider>();
        hpbarLength = Hpbars.Length-1;
        myName.text = myEC.myName;
    }

    public void HpbarValue()
    {
        
        Hpbars[hpbarLength].value = hpbarValue();
        if (myEC.myStat.HP / ((myEC.myStat.maxHp / Hpbars.Length)) < hpbarLength && hpbarLength>0)
        {
            Hpbars[hpbarLength].value = 0;
            hpbarLength--;
            Hpbars[hpbarLength].value = hpbarValue();
        }
    }
    float hpbarValue()
    {
        return 
            myEC.myStat.HP % (myEC.myStat.maxHp / Hpbars.Length) /
            (myEC.myStat.maxHp / Hpbars.Length) ;
    }

}
