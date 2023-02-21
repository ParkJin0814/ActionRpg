using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Inst=null;
    public Transform Status;
    public TMP_Text dropText;
    public Inventory myInventory;
    public Equipment myEquipment;
    public ItemEffectDatabase myItemEffectDatabase;
    public Shop myShop;
    public Slider myHp;
    public Slider mySp;
    [SerializeField] TMP_Text[] myGoldText;
    public int Gold;
    [Header("Quest")]
    public Text talkText;
    public TalkManager talkManager;
    public GameObject TalkImage;
    public GameObject scanObject;
    public Image portraitImage;
    public bool isMove;
    public int talkIndex;
    
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        GoldChange();
    }
    public void ShowText(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        OnTalk(objectData.id, objectData.isNpc);

        TalkImage.SetActive(isMove);
    }
    void OnTalk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isMove = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
            portraitImage.sprite = talkManager.GetSprite(id);
            portraitImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isMove = true;
        talkIndex++;
    }

    public void GoldChange(int a=0)
    {
        Gold += a;
        foreach (var item in myGoldText)
        {
            item.text = $"{Gold}";
        }
    }    
}
