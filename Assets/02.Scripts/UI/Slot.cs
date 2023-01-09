using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField] TMP_Text text_Count;
    [SerializeField] GameObject go_CountImage;
    void SetColor(float alpha)
    {
        Color color= itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int _count=1)
    {

    }
}
