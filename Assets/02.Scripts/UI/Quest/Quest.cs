using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public static bool questActivated = false;
    [SerializeField] private GameObject go_QuestBase;
    [SerializeField] GameObject go_Quest;
    [SerializeField] TMP_Text questName;
    [SerializeField] TMP_Text questDesc;
    [SerializeField] TMP_Text questCount;
    QuestManager questManager;
    private void Start()
    {
        questManager = GameManager.Inst.questManager;
    }
    public void OpenQuest()
    {
        QuestCheck();
        questActivated = true;
        go_QuestBase.SetActive(true);
    }

    public void CloseQuest()
    {
        questActivated = false;
        go_QuestBase.SetActive(false);
    }
    void QuestCheck()
    {

        switch (questManager.questId)
        {
            case 20:
                go_Quest.SetActive(true);
                questCount.text = $"{questManager.questCount}/{questManager.questValue}";
                questName.text = questManager.CheckQuest();
                questDesc.text = "여우들때매 걱정이야 여우좀 잡아줘 \n4마리정도만 \n잡아주면 좋을거같아";
                break;
            default:
                //go_Quest.SetActive(false);
                break;
        }
    }

}
