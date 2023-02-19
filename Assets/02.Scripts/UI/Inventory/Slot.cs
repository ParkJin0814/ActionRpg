using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.PackageManager;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler,
    IPointerEnterHandler,IPointerExitHandler
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
    [SerializeField] bool isQuickSlot;  // �ش� ������ ���������� ���� �Ǵ�
    [SerializeField] int quickSlotNumber;  // ������ �ѹ�
    [SerializeField] bool isEquipmentSlot; // �ش� ������ ��񽽷����� �Ǵ�
    public Item.EquipmentType equipmentType;
    
    private void Start()
    {        
        myInputNumber=FindObjectOfType<InputNumber>();
        myItemEffectDatabase= FindObjectOfType<ItemEffectDatabase>();        
    }
    //���� ����
    void SetColor(float alpha)
    {
        Color color= itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
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

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // �ش� ���� �ϳ� ����
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
                
                //�Һ����̶��
                if (item.itemType == Item.ItemType.Used) 
                {
                    myItemEffectDatabase.UseItem(item);
                    SetSlotCount(-1);
                }
                else if (item.itemType == Item.ItemType.Equipment)
                {
                    if (!isEquipmentSlot)
                    {
                        myItemEffectDatabase.UseItem(item,()=> SetSlotCount(-1));                        
                    }
                    else
                    {                        
                        GameManager.Inst.myInventory.AcquireItem(item);
                        SetSlotCount(-1);
                    }
                }
            }
        }
    }
    
    //�巡�׽���
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item!= null)
        {
            DragSlot.Inst.dragSlot = this;
            DragSlot.Inst.DragSetImage(itemImage);
            DragSlot.Inst.transform.position=eventData.position;
        }
    }
    //�巡����
    public void OnDrag(PointerEventData eventData)
    {
        if(item!= null) DragSlot.Inst.transform.position=eventData.position;
    }
    //�巡�׸� ��������
    public void OnEndDrag(PointerEventData eventData)
    {      
        if (InventoryRectCheck())
        {
            if (DragSlot.Inst.dragSlot != null)
            {
                myInputNumber.transform.position = eventData.position;
                myInputNumber.Call();
            }
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
            (DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x > quickSlotBaseRect.rect.xMin + quickSlotBaseRect.transform.localPosition.x
            && DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x < quickSlotBaseRect.rect.xMax + quickSlotBaseRect.transform.localPosition.x
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y > quickSlotBaseRect.rect.yMin + quickSlotBaseRect.transform.localPosition.y
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y < quickSlotBaseRect.rect.yMax + quickSlotBaseRect.transform.localPosition.y)
            ||
            (DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x > equipmentRect.rect.xMin + equipmentRect.transform.localPosition.x
            && DragSlot.Inst.transform.localPosition.x + inventoryRect.localPosition.x < equipmentRect.rect.xMax + equipmentRect.transform.localPosition.x
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y > equipmentRect.rect.yMin + equipmentRect.transform.localPosition.y
            && DragSlot.Inst.transform.localPosition.y + inventoryRect.transform.localPosition.y < equipmentRect.rect.yMax + equipmentRect.transform.localPosition.y)
            ));
    }
    //�巡���Ͽ� ���Կ� ������
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.Inst.dragSlot != null)
        {
            if(!isEquipmentSlot) ChangeSlot();
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
