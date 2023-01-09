using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //아이템 드랍처리
    [SerializeField] float itemRange; //아이템 습득거리
    bool pickupItme; //아이템 습득가능시 불값
    private RaycastHit hitInfo;  // 충돌체 정보 저장
    [SerializeField] LayerMask ItemLayerMask;
    [SerializeField] TMP_Text dropText;
    [SerializeField] Inventory myInventory;


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
        if (Physics.SphereCast(transform.position, 0.0f, transform.forward, out hitInfo, itemRange, ItemLayerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else ItemInfoDisappear();
    }
    private void ItemInfoAppear()
    {
        pickupItme = true;
        dropText.gameObject.SetActive(true);
        dropText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " Drop " + "<color=yellow>" + "(E)" + "</color>";
    }
    private void ItemInfoDisappear()
    {
        pickupItme = false;
        dropText.gameObject.SetActive(false);
    }
    
    private void CanPickUp()
    {
        if (pickupItme)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 했습니다.");  // 인벤토리 넣기
                myInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }
}
