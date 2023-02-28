using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    public GameObject Boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && GameManager.Inst.questManager.questId==40)
        {
            Boss.SetActive(true);
            Boss.GetComponent<EnemyCharacter>().myTarget = other.transform;
        }        
    }

}
