using System.Collections;
using TMPro;
using UnityEngine;
public class StatValue : MonoBehaviour
{

    public Player myPlayer;
    [SerializeField] TMP_Text[] valueText;
    Coroutine Stat;
    private void OnEnable()
    {
        if (Stat == null)
        {
            Stat = StartCoroutine(StatCall());
        }
        else
        {
            StopAllCoroutines();
            Stat = StartCoroutine(StatCall());
        }
    }
    public void Call()
    {
        valueText[0].text = ((int)myPlayer.myStat.HP).ToString();
        valueText[1].text = ((int)myPlayer.myStat.SP).ToString();
        valueText[2].text = myPlayer.myStat.MoveSpeed.ToString();
        valueText[3].text =
            (myPlayer.myStat.AP + SceneData.Inst.myEquipment.EquipmentSlotValue(Item.EquipmentType.Weapon)).ToString();
    }
    IEnumerator StatCall()
    {
        while (gameObject.activeSelf)
        {
            Call();
            yield return null;
        }
        Stat = null;
    }

}
