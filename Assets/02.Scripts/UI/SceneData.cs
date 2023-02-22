using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneData : MonoBehaviour
{
    public static SceneData Inst = null;
    public Transform Hpbar;
    public Transform FloatingDamage;
    public Inventory myInventory;
    public Equipment myEquipment;
    public Quest myQuest;
    public ItemEffectDatabase myItemEffectDatabase;
    public Shop myShop;
    public Transform Status;
    public TMP_Text dropText;
    public Slider myHp;
    public Slider mySp;
    private void Awake()
    {
        Inst = this;
    }
}
