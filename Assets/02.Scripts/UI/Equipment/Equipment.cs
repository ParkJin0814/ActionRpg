using UnityEngine;
using static Item;

public class Equipment : Base_Window
{
    // 장비창 배열
    public Slot[] slots;
    [SerializeField] Transform parent;
    void Start()
    {
        slots = parent.GetComponentsInChildren<Slot>();
    }

    public int EquipmentSlotValue(EquipmentType E)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].item != null && slots[i].equipmentType == E)
            {
                return slots[i].item.Value;
            }
        }
        return 0;
    }
}
