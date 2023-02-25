using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemEffect
{
    public string itemName;  // �������� �̸�(Key������ ����� ��)
    [Tooltip("HP, SP �� �����մϴ�.")]
    public string[] part;  // ȿ��. ��� �κ��� ȸ���ϰų� Ȥ�� ���� ��������. ���� �ϳ��� ��ġ�� ȿ���� �������� �� �־� �迭.
    public int[] num;  // ��ġ. ���� �ϳ��� ��ġ�� ȿ���� �������� �� �־� �迭. �׿� ���� ��ġ.
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
                                Debug.Log("����");
                                break;
                        }                        
                    }
                    return;
                }
            }
            Debug.Log("itemEffectDatabase�� ��ġ�ϴ� itemName�� �����ϴ�.");
        }
    }
}