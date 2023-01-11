using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputNumber : MonoBehaviour
{
    bool activated = false;
    [SerializeField] GameObject backGround;
    [SerializeField] TMP_Text text_Preview;        
    [SerializeField] TMP_InputField if_text;
    [SerializeField] GameObject go_Base;
    [SerializeField] public static Player myPlayer;
    
    void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Return)) OK();
            else if (Input.GetKeyDown(KeyCode.Escape)) Cancel();
        }
        backGround.SetActive(activated);
    }

    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;        
        if_text.text = "";
        text_Preview.text = DragSlot.Inst.dragSlot.itemCount.ToString();
    }

    public void Cancel()
    {
        activated = false;
        DragSlot.Inst.SetColor(0);
        go_Base.SetActive(false);
        DragSlot.Inst.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.Inst.SetColor(0);        
        int num;        
        if (if_text.text != "")
        {
            if (CheckNumber(if_text.text))
            {
                num = int.Parse(if_text.text);
                if (num > DragSlot.Inst.dragSlot.itemCount)
                    num = DragSlot.Inst.dragSlot.itemCount;                
            }
            else
            {                
                num = 1;
            }
        }
        else num = int.Parse(text_Preview.text);
        
        StartCoroutine(DropItemCorountine(num));
    }
    IEnumerator DropItemCorountine(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            
            Instantiate(DragSlot.Inst.dragSlot.item.itemPrefab,
                myPlayer.transform.position + myPlayer.transform.forward,
                Quaternion.identity);
            DragSlot.Inst.dragSlot.SetSlotCount(-1);
            yield return new WaitForSeconds(0.05f);
        }

        DragSlot.Inst.dragSlot = null;
        go_Base.SetActive(false);
        activated = false;
    }
    private bool CheckNumber(string _argString)
    {
        char[] _tempCharArray = _argString.ToCharArray();
        bool isNumber = true;

        for (int i = 0; i < _tempCharArray.Length; i++)
        {
            if (_tempCharArray[i] >= 48 && _tempCharArray[i] <= 57) continue;
            isNumber = false;
        }
        return isNumber;
    }
}
