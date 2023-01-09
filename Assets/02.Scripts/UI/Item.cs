using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment, //���
        Used, //���ǰ
        Ingredient, // ���
        ETC //��Ÿ
    }
    public string itemName; 
    public ItemType itemType; 
    public Sprite itemImage; // �κ��丮 �̹���
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
    public string weaponType;  // ���� ����
}
