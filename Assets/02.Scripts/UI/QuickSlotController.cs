using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] Slot[] quickSlots;  // �����Ե� (8��)
    [SerializeField] Transform tf_parent;  // �����Ե��� �θ� ������Ʈ

    int selectedSlot;  // ���õ� �������� �ε��� (0~7)
    [SerializeField] GameObject go_SelectedImage;  // ���õ� ������ �̹���
    [SerializeField] ItemEffectDatabase myItemEffectDatabase;     



    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            ChangeSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            ChangeSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            ChangeSlot(7);
    }

    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);        
    }
    public void EatItem()
    {
        if (quickSlots[selectedSlot].item == null) return;
        myItemEffectDatabase.UseItem(quickSlots[selectedSlot].item);
        quickSlots[selectedSlot].SetSlotCount(-1);

        //if (quickSlots[selectedSlot].itemCount <= 0) Destroy(go_HandItem);
    }
    private void SelectedSlot(int _num)
    {
        // ���õ� ����
        selectedSlot = _num;

        // ���õ� �������� �̹��� �̵�
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }
    
}
