using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEffect
{
    public string itemName;  // 아이템의 이름(Key값으로 사용할 것)
    [Tooltip("HP, SP 만 가능합니다.")]
    public string[] part;  // 효과. 어느 부분을 회복하거나 혹은 깎을 포션인지. 포션 하나당 미치는 효과가 여러개일 수 있어 배열.
    public int[] num;  // 수치. 포션 하나당 미치는 효과가 여러개일 수 있어 배열. 그에 따른 수치.
}

public class ItemEffectDatabase : MonoBehaviour
{
    [SerializeField] ItemEffect[] itemEffects;

    private const string HP = "HP", SP = "SP";

    [SerializeField] Player myPlayer;
    [SerializeField] QuickSlotController myQuickSlotController;
    [SerializeField] Equipment myEquipment;

    public void UseItem(Item _item, UnityAction done = null)
    {
        if (_item.itemType == Item.ItemType.Equipment)
        {
            for (int i = 0; i < myEquipment.slots.Length; ++i)
            {
                if (myEquipment.slots[i].equipmentType == _item.equipmentType)
                {
                    if (myEquipment.slots[i].item == null)
                    {
                        myEquipment.slots[i].AddItem(_item);
                        done();
                    }
                    else if (myEquipment.slots[i].item != null)
                    {
                        SceneData.Inst.myInventory.AcquireItem(myEquipment.slots[i].item);
                        myEquipment.slots[i].AddItem(_item);
                        done();
                    }
                }
            }
        }
        if (_item.itemType == Item.ItemType.Used)
        {
            for (int i = 0; i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for (int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch (itemEffects[i].part[j])
                        {
                            case HP:
                                myPlayer.myStat.HP += itemEffects[i].num[j];
                                break;
                            case SP:
                                myPlayer.myStat.SP += itemEffects[i].num[j];
                                break;
                            default:
                                Debug.Log("오류");
                                break;
                        }                        
                    }
                    return;
                }
            }
            Debug.Log("itemEffectDatabase에 일치하는 itemName이 없습니다.");
        }
    }
}