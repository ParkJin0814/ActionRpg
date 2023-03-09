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
        if (myItem != null)
        {
            myItemImage.sprite = myItem.itemImage;
            myItemName.text = myItem.itemName;
            myItemPrice.text = myItem.Price.ToString();
        }
    }

    //���Ź�ư Ŭ���� �Լ�
    public void BuyItem()
    {
        //�����ۿ� ���Ű��� ����� 
        if (GameManager.Inst.Gold >= myItem.Price)
        {
            myshop.myInputNumber.item = myItem;
            if (myItem.itemType != ItemType.Equipment)
            {
                myshop.myInputNumber.Call();
            }
            else
            {
                myshop.myInputNumber.EqCall();
            }
        }        
    }
}
