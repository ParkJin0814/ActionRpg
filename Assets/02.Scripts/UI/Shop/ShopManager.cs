using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] float itemRange; //아이템 습득거리
    private RaycastHit hitInfo;  // 충돌체 정보 저장
    [SerializeField] LayerMask ShopLayerMask;
    [SerializeField] float viewAngle;
    Inventory myInventory;
    // Start is called before the first frame update
    void Start()
    {
        myInventory = SceneData.Inst.myInventory;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
