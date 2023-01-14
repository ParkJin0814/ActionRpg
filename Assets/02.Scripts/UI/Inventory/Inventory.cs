using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �κ��丮 Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.
    public static bool invectoryActivated = false;
    // Inventory_Base �̹���
    [SerializeField] private GameObject go_InventoryBase; 
    // Slot���� �θ��� Grid Setting 
    [SerializeField] private GameObject go_SlotsParent;
    // ���Ե� �迭
    private Slot[] slots;  

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();        
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            invectoryActivated = !invectoryActivated;
            if (invectoryActivated) OpenInventory();
            else CloseInventory();
        }        
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
        GameManager.Inst.GetComponent<ItemEffectDatabase>().HideToolTip();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
