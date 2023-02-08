using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemManager : MonoBehaviour
{
    //������ ���ó��
    [SerializeField] float itemRange; //������ ����Ÿ�
    bool pickupItme; //������ ���氡�ɽ� �Ұ�
    bool shopCheck; //�������Ⱑ�ɽ� �Ұ�
    private RaycastHit hitInfo;  // �浹ü ���� ����
    [SerializeField] LayerMask InteractionLayerMask;    
    [SerializeField] float viewAngle;
    TMP_Text dropText;
    Inventory myInventory;

    private void Start()
    {
        dropText = GameManager.Inst.dropText;
        myInventory= GameManager.Inst.myInventory;
    }
    private void Update()
    {
        DropItem();
    }
    protected void DropItem()
    {
        TryAction();
        CheckItem();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, itemRange, InteractionLayerMask);
        if (_target.Length == 0)
        {
            if(dropText.gameObject.activeSelf) ItemInfoDisappear();
            return;
        }
        foreach(Collider collider in _target)
        {
            Vector3 _direction = (collider.transform.position - transform.position).normalized;
            float _angle = Vector3.Angle(_direction, transform.forward);
            if (_angle < viewAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position + transform.up, _direction, out hitInfo, itemRange))
                {                    
                    if (hitInfo.transform.tag == "Item")
                    {
                        ItemInfoAppear();
                    }
                    else if (hitInfo.transform.tag=="Shop")
                    {
                        ShopInfoApperar();
                    }
                }
            }
            else if (dropText.gameObject.activeSelf) ItemInfoDisappear();
        }
    }
    private void ItemInfoAppear()
    {
        pickupItme = true;
        dropText.gameObject.SetActive(true);
        dropText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName +"\n" +"<color=yellow>" + "(E)" + "</color>";
    }
    private void ShopInfoApperar()
    {
        shopCheck= true;
        dropText.gameObject.SetActive(true);
        dropText.text = "Shop" + "\n" + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear()
    {
        pickupItme = false;
        shopCheck = false;
        dropText.gameObject.SetActive(false);
    }
    
    private void CanPickUp()
    {
        if (pickupItme)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ�� �߽��ϴ�.");  // �κ��丮 �ֱ�
                myInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
        else if(shopCheck)
        {
            GameManager.Inst.myShop.OpenShop();
            ItemInfoDisappear();
        }
    }
}
