using UnityEngine;

public class Shop : Base_Window
{
    [SerializeField] Item[] BuyItem;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject myShopSlot;
    public BuyInputNumber myInputNumber;

    void Start()
    {
        for (int i = 0; i < BuyItem.Length; ++i)
        {
            GameObject obj = Instantiate(myShopSlot, itemParent);
            obj.GetComponent<ShopSlot>().myItem = BuyItem[i];
            obj.GetComponent<ShopSlot>().myshop = this;
        }
    }
}
