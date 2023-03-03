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

    [SerializeField] GameObject eqBase;
    [SerializeField] TMP_Text eqText;    
    void Start()
    {
        myInventory = SceneData.Inst.myInventory;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {

            if (Input.GetKeyDown(KeyCode.Return)) Ok();
            else if (Input.GetKeyDown(KeyCode.Escape)) Cancel();
            int a = int.Parse(if_text.text);
            if (item != null && GameManager.Inst.Gold / item.Price < a)
            {
                if_text.text = (GameManager.Inst.Gold / item.Price).ToString();
            }
        }

    }
    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;
        if_text.text = "1";
    }
    public void EqCall()
    {
        eqBase.SetActive(true);
        eqText.text = $"{item.itemName} 아이템을 {item.Price}골드에 구매하시겠습니까?";
    }
    public void EqCancel()
    {
        eqBase.SetActive(false);
        item = null;
    }
    public void EqOk()
    {
        if (GameManager.Inst.Gold > item.Price)
        {
            myInventory.AcquireItem(item);
            GameManager.Inst.GoldChange(-item.Price);
        }
        EqCancel();
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
    public void PlusMinusButton(bool v)
    {
        if (v)
        {
            if_text.text = (int.Parse(if_text.text) + 1).ToString();
        }
        else
        {
            if ((int.Parse(if_text.text) - 1) >= 0)
                if_text.text = (int.Parse(if_text.text) - 1).ToString();
        }
    }
}
