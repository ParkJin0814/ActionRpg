using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyInputNumber : MonoBehaviour
{

    
    bool activated = false;
    [SerializeField] TMP_Text text_Preview;
    [SerializeField] TMP_InputField if_text;
    [SerializeField] GameObject go_Base;
    public Item item;
    Inventory myInventory;
    void Start()
    {
        myInventory = GameManager.Inst.myInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return)) Ok();
            else if (Input.GetKeyDown(KeyCode.Escape)) Cancel();
            if (item != null && GameManager.Inst.Gold / item.Price < int.Parse(if_text.text))
            {
                if_text.text = (GameManager.Inst.Gold / item.Price).ToString();
            }
        }
        
    }
    public void Call()
    {
        go_Base.SetActive(true);
        activated=true;
        if_text.text = "1";
    }
    public void Cancel()
    {
        activated = false;        
        go_Base.SetActive(false);
        item = null;
    }
    public void Ok()
    {
        if (GameManager.Inst.Gold > int.Parse(if_text.text) * item.Price)
        {
            myInventory.AcquireItem(item, int.Parse(if_text.text));
            GameManager.Inst.GoldChange(-(int.Parse(if_text.text) * item.Price));
        }
        Cancel();
    }
}
