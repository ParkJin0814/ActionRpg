using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] float itemRange; //������ ����Ÿ�
    private RaycastHit hitInfo;  // �浹ü ���� ����
    [SerializeField] LayerMask ShopLayerMask;
    [SerializeField] float viewAngle;
    Inventory myInventory;
    // Start is called before the first frame update
    void Start()
    {
        myInventory = GameManager.Inst.myInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
