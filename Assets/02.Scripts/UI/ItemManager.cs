using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //아이템 드랍처리
    [SerializeField] float itemRange; //아이템 습득거리
    bool pickupItme; //아이템 습득가능시 불값
    bool NpcCheck; //상점열기가능시 불값
    private RaycastHit hitInfo;  // 충돌체 정보 저장
    [SerializeField] LayerMask InteractionLayerMask;
    [SerializeField] float viewAngle;
    [SerializeField] GameObject DropButton; //드랍템 획득버튼
    TMP_Text dropText;
    Inventory myInventory;

    private void Start()
    {
        dropText = SceneData.Inst.dropText;
        myInventory = SceneData.Inst.myInventory;
    }
    private void Update()
    {
        CheckItem();
    }

    private void CheckItem()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, itemRange, InteractionLayerMask);
        if (_target.Length == 0)
        {
            ItemInfoDisappear();
            return;
        }
        foreach (Collider collider in _target)
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
                    else if (hitInfo.transform.tag == "Npc")
                    {
                        NpcInfoApperar();
                    }
                }
            }
            else if (DropButton.activeSelf) ItemInfoDisappear();
        }
    }
    private void ItemInfoAppear()
    {
        pickupItme = true;
        DropButton.SetActive(true);
        dropText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName;
    }
    private void NpcInfoApperar()
    {
        NpcCheck = true;
        DropButton.SetActive(true);
        dropText.text = "대화";
    }
    private void ItemInfoDisappear()
    {
        pickupItme = false;
        NpcCheck = false;
        DropButton.SetActive(false);
    }

    public void CanPickUp()
    {
        if (pickupItme)
        {
            if (hitInfo.transform != null)
            {
                myInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                if (hitInfo.transform.GetComponent<ItemPickUp>().item.itemName == "암흑물질")
                {
                    GameManager.Inst.questManager.QuestCountCheck();
                }
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
        else if (NpcCheck)
        {
            GameManager.Inst.Action(hitInfo.transform.gameObject);
            GameManager.Inst.talkPanel.GetComponent<Button>().onClick.RemoveAllListeners();
            GameManager.Inst.talkPanel.GetComponent<Button>().onClick.AddListener(() => GameManager.Inst.Action(hitInfo.transform.gameObject));
            hitInfo.transform.GetComponent<ObjData>().OnTalk();
            ItemInfoDisappear();
        }
    }
}
