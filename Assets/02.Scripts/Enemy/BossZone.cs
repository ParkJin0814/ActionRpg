using UnityEngine;

public class BossZone : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Cinema;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && GameManager.Inst.questManager.questId == 40)
        {
            Cinema.SetActive(true);
            Boss.GetComponent<EnemyCharacter>().myTarget = other.transform;
        }
    }

}
