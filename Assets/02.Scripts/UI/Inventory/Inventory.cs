using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 인벤토리 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.
    public static bool invectoryActivated = false;
    // Inventory_Base 이미지
    [SerializeField] private GameObject go_InventoryBase; 
    // Slot들의 부모인 Grid Setting 
    [SerializeField] private GameObject go_SlotsParent;
    // 슬롯들 배열
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
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
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
