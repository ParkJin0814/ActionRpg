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

    //구매버튼 클릭시 함수
    public void BuyItem()
    {
        //아이템에 구매가격 만들기 
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
