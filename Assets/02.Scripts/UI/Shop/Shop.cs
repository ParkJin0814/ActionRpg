using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] Item[] BuyItem;
    [SerializeField] Transform itemParent;
    [SerializeField] GameObject myShopSlot;
    [SerializeField] GameObject myShopbase;
    public BuyInputNumber myInputNumber;
    public static bool shopActivated = false;

    void Start()
    {
        for(int i=0 ; i < BuyItem.Length ; ++i)
        {
            GameObject obj=Instantiate(myShopSlot, itemParent);
            obj.GetComponent<ShopSlot>().myItem= BuyItem[i];
            obj.GetComponent<ShopSlot>().myshop = this;
        }
    }
    public void OpenShop()
    {
        shopActivated = true;
        myShopbase.SetActive(true);        
    }
    public void CloseShop()
    {
        shopActivated = false;
        myShopbase.SetActive(false);
        
    }
}
