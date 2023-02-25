using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot Inst;
    public Slot dragSlot;

    [SerializeField] Image itemImage;
    private void Awake()
    {
        Inst = this;
    }
    public void DragSetImage(Image _itemImage)
    {
        itemImage.sprite = _itemImage.sprite;
        SetColor(1);
    }
    public void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
