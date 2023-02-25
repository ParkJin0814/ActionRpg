using UnityEngine;
using static Item;

public class Equipment : MonoBehaviour
{
    // 장비창 활성화 여부. true가 되면 카메라 움직임과 다른 입력을 막을 것이다.
    public static bool equipmentActivated = false;
    // Inventory_Base 이미지
    [SerializeField] private GameObject go_EquipmentBase;
    // 장비창 배열
    public Slot[] slots;
    [SerializeField] Transform parent;
    void Start()
    {
        slots = parent.GetComponentsInChildren<Slot>();
    }

    void Update()
    {
        TryOpenEquipment();
    }

    private void TryOpenEquipment()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (!equipmentActivated) OpenEquipment();
            else CloseEquipment();
        }
        if (equipmentActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseEquipment();
        }
    }

    public void OpenEquipment()
    {
        equipmentActivated = true;
        go_EquipmentBase.SetActive(true);

    }

    public void CloseEquipment()
    {
        equipmentActivated = false;
        go_EquipmentBase.SetActive(false);
    }
    public int EquipmentSlotValue(EquipmentType E)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i].item != null && slots[i].equipmentType == E)
            {
                return slots[i].item.Value;
            }
        }
        return 0;
    }
}
