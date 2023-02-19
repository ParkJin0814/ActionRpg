using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,  //���
        Used,       //���ǰ
        Ingredient, //���
        ETC         //��Ÿ
    }
    public enum EquipmentType
    {
        None,
        Weapon, //����
        Gloves, //�尩
        Shoes,  //�Ź�
        Socks,  //�縻
        Ring,   //����
        Helmet  //����
    }
    public string itemName; 
    public ItemType itemType;
    public EquipmentType equipmentType;
    public Sprite itemImage; // �κ��丮 �̹���
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)
    public GameObject[] weaponPrefab; //�������� ������ 0 �޼� 1 ������
    public string weaponType;  // ���� ����    
    public int Price;
    [TextArea] public string itemDesc;
}
