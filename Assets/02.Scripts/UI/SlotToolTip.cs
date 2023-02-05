using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField] GameObject myBase;
    [SerializeField] TMP_Text ItemName;
    [SerializeField] TMP_Text ItemDesc;
    [SerializeField] TMP_Text ItemHowtoUsed;
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        myBase.SetActive(true);
        _pos += new Vector3(-myBase.GetComponent<RectTransform>().rect.width*0.4f, 
            +myBase.GetComponent<RectTransform>().rect.height * 0.35f, 0);
        myBase.transform.position = _pos;

        ItemName.text = _item.itemName;
        ItemDesc.text = _item.itemDesc;

        if (_item.itemType == Item.ItemType.Equipment)
            ItemHowtoUsed.text = "RightClick-Equipped";
        else if (_item.itemType == Item.ItemType.Used)
            ItemHowtoUsed.text = "RightClick - Use";
        else
            ItemHowtoUsed.text = "";
    }

    public void HideToolTip()
    {
        myBase.SetActive(false);
    }
}
