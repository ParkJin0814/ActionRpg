using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Inst = null;
    public GameObject[] GameObj;
    [SerializeField] TMP_Text[] myGoldText;
    public int Gold;
    [Header("Talk")]
    public GameObject talkPanel;
    public TMP_Text talkText;
    public GameObject scanObject;
    public TalkManager talkManager;
    public Image portraitImag;
    public bool isAction;
    public int talkIndex;
    [Header("Quest")]
    public QuestManager questManager;
    private void Awake()
    {
        Inst = this;
    }
    private void Start()
    {
        GoldChange();
    }
    public void Action(GameObject scanObj)
    {

        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNPC);

        talkPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        
        //다음대화가 없다면
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questManager.CheckQuest(id);
            OpenNpc(id, questTalkIndex);
            return;
        }
        //다음대화
        if (isNpc)
        {
            talkText.text = talkData;

            portraitImag.sprite = talkManager.GetPortrait(id, 0);
            portraitImag.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
    public void GoldChange(int a = 0)
    {
        Gold += a;
        foreach (var item in myGoldText)
        {
            item.text = $"{Gold}";
        }
    }
    void OpenNpc(int id, int questTalkIndex)
    {
        if (id == 1000)
        {
            SceneData.Inst.myShop.OpenShop();
        }
        else if (id + questTalkIndex == 2010)
        {
            SceneData.Inst.myQuest.OpenQuest();
        }
    }
}
