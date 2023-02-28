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
        go_Quest.SetActive(true);
        questCount.text = $"{questManager.questCount}/{questManager.questValue}";
        questName.text = questManager.CheckQuest();
        switch (questManager.questId)
        {
            case 20:                
                questDesc.text = "����鶧�� �����̾� ������ ����� \n4���������� \n����ָ� �����Ű���";
                break;
            case 40:                
                questDesc.text = "�������� �̻��� �Ҹ��� �鸮�°� ���� Ȯ���ϰ� ���ƿ���";
                break;
            default:
                go_Quest.SetActive(false);
                break;
        }
    }

}
