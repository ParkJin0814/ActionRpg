using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,  //장비
        Used,       //사용품
        Ingredient, //재료
        ETC         //기타
    }
    public enum EquipmentType
    {
        None,
        Weapon, //무기
        Gloves, //장갑
        Shoes,  //신발
        Socks,  //양말
        Ring,   //반지
        Helmet  //투구
    }
    public string itemName;
    public ItemType itemType;
    public EquipmentType equipmentType; //장비라면 장비타입까지설정 아닌경우 None
    public Sprite itemImage; // 인벤토리 이미지
    public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)     
    public int Value; //능력 무기라면 공격력
    public int Price; //가격
    [TextArea] public string itemDesc; //아이템설명
}
