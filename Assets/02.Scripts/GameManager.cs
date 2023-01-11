using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Inst=null;
    public Transform Status;
    public TMP_Text dropText;
    public Inventory myInventory;

    public Slider myHp;
    public Slider mySp;
    private void Awake()
    {
        Inst = this;
    }
}
