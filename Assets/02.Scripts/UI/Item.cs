using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment, //장비
        Used, //사용품
        Ingredient, // 재료
        ETC //기타
    }
    public string itemName; 
    public ItemType itemType; 
    public Sprite itemImage; // 인벤토리 이미지
    public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)
    public string weaponType;  // 무기 유형
}
