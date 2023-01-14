using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    [SerializeField] Slot[] quickSlots;  // 퀵슬롯들 (8개)
    [SerializeField] Transform tf_parent;  // 퀵슬롯들의 부모 오브젝트

    int selectedSlot;  // 선택된 퀵슬롯의 인덱스 (0~7)
    [SerializeField] GameObject go_SelectedImage;  // 선택된 퀵슬롯 이미지
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
        // 선택된 슬롯
        selectedSlot = _num;

        // 선택된 슬롯으로 이미지 이동
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }
    
}
