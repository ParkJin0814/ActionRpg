using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ShopSlot : MonoBehaviour
{
    public Shop myshop;
    public Item myItem;
    [SerializeField] Image myItemImage;
    [SerializeField] TMP_Text myItemName;
    [SerializeField] TMP_Text myItemPrice;
    
    void Start()
    {
        if(myItem!=null)
        {
            myItemImage.sprite = myItem.itemImage;
            myItemName.text=myItem.name;
            myItemPrice.text = myItem.Price.ToString();
        }        
    }
    
    //���Ź�ư Ŭ���� �Լ�
    public void BuyItem()
    {
        //�����ۿ� ���Ű��� ����� 
        if (GameManager.Inst.Gold >= myItem.Price)
        {
            if (myItem.itemType != ItemType.Equipment)
            {
                myshop.myInputNumber.Call();
                myshop.myInputNumber.item = myItem;
            }
            else
            {
                SceneData.Inst.myInventory.AcquireItem(myItem);
                GameManager.Inst.GoldChange(-myItem.Price);
            }
        }
        else
        {
            //�ܾ׺���
        }
    }
}
