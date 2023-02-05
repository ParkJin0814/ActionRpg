using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Inst=null;
    public Transform Status;
    public TMP_Text dropText;
    public Inventory myInventory;
    public Shop myShop;

    public Slider myHp;
    public Slider mySp;
    [SerializeField] TMP_Text[] myGoldText;
    public int Gold;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        GoldChange();
    }
    
    public void GoldChange(int a=0)
    {
        Gold += a;
        foreach (var item in myGoldText)
        {
            item.text = $"{Gold}";
        }
    }

    public bool MousePointCheck()
    {

        if (Inventory.invectoryActivated ||
            Shop.shopActivated)
        {
            return false;
        }
        return true;
    }
    public void MouseCussorCheck()
    {
        if (MousePointCheck())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
