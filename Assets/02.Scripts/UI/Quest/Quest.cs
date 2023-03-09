using TMPro;
using UnityEngine;

public class Quest : Base_Window
{
    [SerializeField] GameObject go_Quest;
    [SerializeField] TMP_Text questName;
    [SerializeField] TMP_Text questDesc;
    [SerializeField] TMP_Text questCount;
    QuestManager questManager;
    private void Start()
    {
        questManager = GameManager.Inst.questManager;
        openAction = () => { QuestCheck(); };
    }
    void QuestCheck()
    {
        go_Quest.SetActive(true);
        questCount.text = $"{questManager.questCount}/{questManager.questValue}";
        questName.text = questManager.CheckQuest();
        switch (questManager.questId)
        {
            case 20:
                questDesc.text = "여우들때매 걱정이야 여우좀 잡아줘 \n4마리정도만 \n잡아주면 좋을거같아";
                break;
            case 40:
                questDesc.text = "공원에서 이상한 소리가 들리는거 같아 확인하고 돌아와줘";
                break;
            default:
                go_Quest.SetActive(false);
                break;
        }
    }

}
