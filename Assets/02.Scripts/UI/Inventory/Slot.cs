using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler,
    IPointerEnterHandler,IPointerExitHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField] RectTransform inventoryRect;
    [SerializeField] RectTransform quickSlotBaseRect;
    InputNumber myInputNumber;
    ItemEffectDatabase myItemEffectDatabase;
    [SerializeField] TMP_Text text_Count;
    [SerializeField] GameObject go_CountImage;
    [SerializeField] bool isQuickSlot;  // 해당 슬롯이 퀵슬롯인지 여부 판단
    [SerializeField] int quickSlotNumber;  // 퀵슬롯 넘버
    private void Start()
    {        
        myInputNumber=FindObjectOfType<InputNumber>();
        myItemEffectDatabase= FindObjectOfType<ItemEffectDatabase>();        
    }
    //투명도 조절
    void SetColor(float alpha)
    {
        Color color= itemImage.color;
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

        if (itemCount <= 0)
        {
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
            if (item != null)
            {
                myItemEffectDatabase.UseItem(item);
                //소비템이라면
                if (item.itemType == Item.ItemType.Used) 
                {                    
                    SetSlotCount(-1);
                }
            }
        }
    }
    
    //드래그시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item!= null)
        {
            DragSlot.Inst.dragSlot = this;
            DragSlot.Inst.DragSetImage(itemImage);
            DragSlot.Inst.transform.position=eventData.position;
        }
    }
    //드래그중
    public void OnDrag(PointerEventData eventData)
    {
        if(item!= null) DragSlot.Inst.transform.position=eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {      
        if (InventoryRectCheck())
        {
            if (DragSlot.Inst.dragSlot != null) myInputNumber.Call();
        }
        else
        {
            DragSlot.Inst.SetColor(0);
            DragSlot.Inst.dragSlot = null;
        }
    }
    bool InventoryRectCheck()
    {
        return (!((DragSlot.Inst.transform.localPosition.x > inventoryRect.rect.xMin
            && DragSlot.Inst.transform.localPosition.x < inventoryRect.rect.xMax
            && DragSlot.Inst.transform.localPosition.y > inventoryRect.rect.yMin
            && DragSlot.Inst.transform.localPosition.y < inventoryRect.rect.yMax)
            ||
            (DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x > quickSlotBaseRect.rect.xMin
            && DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x < quickSlotBaseRect.rect.xMax
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y > quickSlotBaseRect.rect.yMin + quickSlotBaseRect.transform.localPosition.y
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y < quickSlotBaseRect.rect.yMax + quickSlotBaseRect.transform.localPosition.y)));
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.Inst.dragSlot != null)
        {
            ChangeSlot();            
        }
    }
    void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddItem(DragSlot.Inst.dragSlot.item,DragSlot.Inst.dragSlot.itemCount);
        if(tempItem!=null) DragSlot.Inst.dragSlot.AddItem(tempItem,tempItemCount);
        else DragSlot.Inst.dragSlot.ClearSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null) myItemEffectDatabase.ShowToolTip(item,transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myItemEffectDatabase.HideToolTip();
    }
    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }
}
