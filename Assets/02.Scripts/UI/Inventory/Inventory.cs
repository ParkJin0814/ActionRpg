using TMPro;
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
    public ItemInspector myItemInspector;
    public TMP_Text PotionCount;

    void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == "Potion")
                {
                    PotionCount.text = slots[i].itemCount.ToString();
                }
            }
        }
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!invectoryActivated) OpenInventory();
            else CloseInventory();
        }
        if (invectoryActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        GameManager.Inst.ButtonClick();
        invectoryActivated = true;
        go_InventoryBase.SetActive(true);
    }

    public void CloseInventory()
    {
        GameManager.Inst.ButtonClick();
        invectoryActivated = false;
        go_InventoryBase.SetActive(false);
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
                        if (_item.name == "Potion")
                        {
                            PotionCount.text = slots[i].itemCount.ToString();
                        }
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
                if (_item.name == "Potion")
                {
                    PotionCount.text = slots[i].itemCount.ToString();
                }
                return;
            }
        }

    }

    public void UsePotion()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == "Potion")
                {

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
    }
}
