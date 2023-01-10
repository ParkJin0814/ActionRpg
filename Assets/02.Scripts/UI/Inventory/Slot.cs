using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField] TMP_Text text_Count;
    [SerializeField] GameObject go_CountImage;

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
            ClearSlot();
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
                    Debug.Log(item.itemName + " �� ����߽��ϴ�.");
                    SetSlotCount(-1);
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

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.Inst.SetColor(0);
        DragSlot.Inst.dragSlot=null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.Inst.dragSlot !=null)ChangeSlot();
    }
    void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddItem(DragSlot.Inst.dragSlot.item,DragSlot.Inst.dragSlot.itemCount);
        if(tempItem!=null) DragSlot.Inst.dragSlot.AddItem(tempItem,tempItemCount);
        else DragSlot.Inst.dragSlot.ClearSlot();
    }
}
