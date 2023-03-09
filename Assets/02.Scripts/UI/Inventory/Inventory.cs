using TMPro;
using UnityEngine;

public class Inventory : Base_Window
{
    // Slot���� �θ��� Grid Setting 
    [SerializeField] private GameObject go_SlotsParent;
    // ���Ե� �迭
    private Slot[] slots;
    public ItemInspector myItemInspector;
    public TMP_Text PotionCount;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) continue;
            if (slots[i].item.itemName == "Potion")
                PotionCount.text = slots[i].itemCount.ToString();
        }
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
                if (slots[i].item == null) continue;
                if (slots[i].item.itemName != _item.itemName) continue;
                slots[i].SetSlotCount(_count);
                if (_item.name == "Potion")
                    PotionCount.text = slots[i].itemCount.ToString();
                return;
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null) continue;
            slots[i].AddItem(_item, _count);
            if (_item.name == "Potion")
                PotionCount.text = slots[i].itemCount.ToString();
            return;
        }
    }

    public void UsePotion()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null) continue;
            if (slots[i].item.itemName != "Potion") continue;
            if (slots[i].itemCount > 0)
            {
                SceneData.Inst.myItemEffectDatabase.UseItem(slots[i].item);
                slots[i].SetSlotCount(-1);
                PotionCount.text = slots[i].itemCount.ToString();
            }
            else PotionCount.text = "0";
            return;
        }
    }
}
