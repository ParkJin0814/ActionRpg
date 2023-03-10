using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField] RectTransform inventoryRect;
    [SerializeField] RectTransform quickSlotBaseRect;
    [SerializeField] RectTransform equipmentRect;
    InputNumber myInputNumber;
    ItemEffectDatabase myItemEffectDatabase;
    [SerializeField] TMP_Text text_Count;
    [SerializeField] GameObject go_CountImage;
    [SerializeField] bool isQuickSlot;  // 해당 슬롯이 퀵슬롯인지 여부 판단
    [SerializeField] int quickSlotNumber;  // 퀵슬롯 넘버
    public bool isEquipmentSlot; // 해당 슬롯이 장비슬롯인지 판단
    public Item.EquipmentType equipmentType;
    ItemInspector myItemInspector;

    private void Start()
    {
        myInputNumber = FindObjectOfType<InputNumber>();
        myItemInspector = SceneData.Inst.myInventory.myItemInspector;
        myItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();
    }
    //투명도 조절
    void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // 인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // 해당 슬롯의 아이템 갯수 업데이트
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (item.name == "Potion")
        {
            SceneData.Inst.myInventory.PotionCount.text = itemCount.ToString();
        }
        if (itemCount <= 0)
        {
            SceneData.Inst.myInventory.myItemInspector.Close();
            ClearSlot();
        }
    }

    // 해당 슬롯 하나 삭제
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
        if (item != null)
        {
            GameManager.Inst.ButtonClick();
            myItemInspector.item = item;
            myItemInspector.mySlot = this;
            myItemInspector.Call();
        }
    }
    public void UseItem()
    {
        if (item != null)
        {
            //소비템이라면
            if (item.itemType == Item.ItemType.Used)
            {
                myItemEffectDatabase.UseItem(item);
                SetSlotCount(-1);

            }
            else if (item.itemType == Item.ItemType.Equipment)
            {
                if (!isEquipmentSlot)
                {
                    myItemEffectDatabase.UseItem(item, () => SetSlotCount(-1));
                }
                else
                {
                    SceneData.Inst.myInventory.AcquireItem(item);
                    SetSlotCount(-1);
                }
            }
        }
    }
    public void DropItem()
    {
        if (item != null)
        {
            DragSlot.Inst.dragSlot = this;
            myInputNumber.Call();
        }
    }
    //드래그시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.Inst.dragSlot = this;
            DragSlot.Inst.DragSetImage(itemImage);
            DragSlot.Inst.transform.position = eventData.position;
        }
    }
    //드래그중
    public void OnDrag(PointerEventData eventData)
    {
        if (item != null) DragSlot.Inst.transform.position = eventData.position;
    }
    //드래그를 놓았을때
    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Inst.SetColor(0);
        DragSlot.Inst.dragSlot = null;
    }

    //드래그하여 슬롯에 놓을때
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.Inst.dragSlot != null)
        {
            if (!isEquipmentSlot) ChangeSlot();
            else if (DragSlot.Inst.dragSlot.item.equipmentType == equipmentType)
            {
                ChangeSlot();
            }
        }
    }
    void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddItem(DragSlot.Inst.dragSlot.item, DragSlot.Inst.dragSlot.itemCount);
        if (tempItem != null) DragSlot.Inst.dragSlot.AddItem(tempItem, tempItemCount);
        else DragSlot.Inst.dragSlot.ClearSlot();
    }
    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }
}
