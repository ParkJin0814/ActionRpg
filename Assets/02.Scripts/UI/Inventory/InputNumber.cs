using System.Collections;
using TMPro;
using UnityEngine;

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
            if (if_text.text != "")
            {
                if (int.Parse(if_text.text) > DragSlot.Inst.dragSlot.itemCount)
                {
                    if_text.text = DragSlot.Inst.dragSlot.itemCount.ToString();
                }
            }
        }
        backGround.SetActive(activated);

    }

    public void Call()
    {
        go_Base.SetActive(true);
        activated = true;

        text_Preview.text = DragSlot.Inst.dragSlot.itemCount.ToString();
        if_text.text = text_Preview.text;
    }

    public void Cancel()
    {
        GameManager.Inst.ButtonClick();
        activated = false;
        DragSlot.Inst.SetColor(0);
        go_Base.SetActive(false);
        DragSlot.Inst.dragSlot = null;
    }

    public void OK()
    {
        DragSlot.Inst.SetColor(0);
        GameManager.Inst.ButtonClick();
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
                Quaternion.identity, SceneData.Inst.dropObject);
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
