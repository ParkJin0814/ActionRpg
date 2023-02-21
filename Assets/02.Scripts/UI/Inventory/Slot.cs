using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    [SerializeField] RectTransform inventoryRect;
    [SerializeField] RectTransform quickSlotBaseRect;
    [SerializeField] RectTransform equipmentRect;
    InputNumber myInputNumber;
    ItemEffectDatabase myItemEffectDatabase;
    [SerializeField] TMP_Text text_Count;
    [SerializeField] GameObject go_CountImage;
    [SerializeField] bool isQuickSlot;  // �ش� ������ ���������� ���� �Ǵ�
    [SerializeField] int quickSlotNumber;  // ������ �ѹ�
    public bool isEquipmentSlot; // �ش� ������ ��񽽷����� �Ǵ�
    public Item.EquipmentType equipmentType;
    ItemInspector myItemInspector;
    
    private void Start()
    {        
        myInputNumber=FindObjectOfType<InputNumber>();
        myItemInspector = GameManager.Inst.myInventory.myItemInspector;
        myItemEffectDatabase = FindObjectOfType<ItemEffectDatabase>();        
    }
    //���� ����
    void SetColor(float alpha)
    {
        Color color= itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // �κ��丮�� ���ο� ������ ���� �߰�
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // �ش� ������ ������ ���� ������Ʈ
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (item.name == "Potion")
        {
            GameManager.Inst.myInventory.PotionCount.text = itemCount.ToString();
        }        
        if (itemCount <= 0)
        {
            if(myItemInspector.gameObject.activeSelf)myItemInspector.Close();
            ClearSlot();            
        }
    }

    // �ش� ���� �ϳ� ����
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
        if(item!= null)
        {
            myItemInspector.item = item;
            myItemInspector.mySlot = this;
            myItemInspector.Call();
        }
    }
    public void UseItem()
    {
        if (item != null)
        {
            //�Һ����̶��
            if (item.itemType == Item.ItemType.Used)
            {
                myItemEffectDatabase.UseItem(item);
                SetSlotCount(-1);
                
            }
            else if (item.itemType == Item.ItemType.Equipment)
            {
                if (!isEquipmentSlot)
                {
                    myItemEffectDatabase.UseItem(item, () => SetSlotCount(-1));
                }
                else
                {
                    GameManager.Inst.myInventory.AcquireItem(item);
                    SetSlotCount(-1);                    
                }
            }
        }        
    }
    public void DropItem()
    {
        if (item != null)
        {
            DragSlot.Inst.dragSlot = this;
            myInputNumber.Call();
        }
    }
    //�巡�׽���
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item!= null)
        {
            DragSlot.Inst.dragSlot = this;
            DragSlot.Inst.DragSetImage(itemImage);
            DragSlot.Inst.transform.position=eventData.position;
        }
    }
    //�巡����
    public void OnDrag(PointerEventData eventData)
    {
        if(item!= null) DragSlot.Inst.transform.position=eventData.position;
    }
    //�巡�׸� ��������
    public void OnEndDrag(PointerEventData eventData)
    {   
        DragSlot.Inst.SetColor(0);
        DragSlot.Inst.dragSlot = null;
    }
    
    //�巡���Ͽ� ���Կ� ������
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.Inst.dragSlot != null)
        {
            if(!isEquipmentSlot) ChangeSlot();
            else if (DragSlot.Inst.dragSlot.item.equipmentType == equipmentType)
            {
                ChangeSlot();
            }
        }
    }
    void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddItem(DragSlot.Inst.dragSlot.item,DragSlot.Inst.dragSlot.itemCount);
        if(tempItem!=null) DragSlot.Inst.dragSlot.AddItem(tempItem,tempItemCount);
        else DragSlot.Inst.dragSlot.ClearSlot();
    }
    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }
}
