using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInspector : MonoBehaviour
{
    public Item item;
    [SerializeField] Image ItemImage;
    [SerializeField] GameObject ItemInspector_base;
    [SerializeField] TMP_Text ItemDesc;
    [SerializeField] TMP_Text ItemName;
    [SerializeField] TMP_Text UseButtonText;
    public Slot mySlot = null;
    public void Call()
    {
        ItemInspector_base.SetActive(true);
        if(item!= null) 
        {
            ItemImage.sprite = item.itemImage;
            ItemName.text= item.name;
            ItemDesc.text = item.itemDesc;
            if (mySlot != null && mySlot.isEquipmentSlot) UseButtonText.text = "해제";
            else UseButtonText.text = "사용";
            SetColor(1);
        }
    }
    public void Close()
    {
        if (ItemInspector_base.activeSelf)
        {
            SetColor(0);
            ItemInspector_base.SetActive(false);
            item = null;
            ItemImage.sprite = null;
            ItemName.text = "";
            ItemDesc.text = "";
            mySlot = null;            
        }
    }
    void SetColor(float alpha)
    {
        Color color = ItemImage.color;
        color.a = alpha;
        ItemImage.color = color;
    }
    public void UseButton()
    {
        if(mySlot!=null)
        {
            if(mySlot.item!=null)
            {
                mySlot.UseItem();
            }
        }
    }
    public void DropItem()
    {
        if(mySlot!=null)
        {
            mySlot.DropItem();            
        }
    }
}
