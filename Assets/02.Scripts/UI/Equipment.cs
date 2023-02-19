using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    // ���â Ȱ��ȭ ����. true�� �Ǹ� ī�޶� �����Ӱ� �ٸ� �Է��� ���� ���̴�.
    public static bool equipmentActivated = false;
    // Inventory_Base �̹���
    [SerializeField] private GameObject go_EquipmentBase;    
    // ���â �迭
    public Slot[] slots;
    [SerializeField] Transform parent;
    void Start()
    {
        slots= parent.GetComponentsInChildren<Slot>();
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
        GameManager.Inst.GetComponent<ItemEffectDatabase>().HideToolTip();
        
    }
}
