using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;
    public bool questCheck = true;
    public string questMobName;
    public int questValue;
    public int questCount = 0;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    void GenerateData()
    {
        questList.Add(10, new QuestData("���츦 �����", new int[] { 2000 }));
        questList.Add(20, new QuestData("���츦 �����", new int[] { 2000, 2000 }));
        questList.Add(30, new QuestData("���츦 ���������Ʈ Ŭ����!", new int[] { 0 }));

    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {
        //��������Ʈ��ȭ
        if (id == questList[questId].npcId[questActionIndex] && questCheck)
            questActionIndex++;

        //����Ʈ ����
        ControlQuest(id);

        //���������Ȳ�̾��ٸ�
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }
    public string CheckQuest()
    {
        return questList[questId].questName;
    }
    void NextQuest()
    {
        if (questCheck)
        {
            questId += 10;
            questActionIndex = 0;
            questCount = 0;
            questMobName = "";
        }
    }
    void ControlQuest(int id)
    {
        switch (questId)
        {
            case 10:
                if (id == questList[questId].npcId[0])
                {
                    NextQuest();
                    questCheck = false;
                    questMobName = "Fox";
                    questValue = 4;
                }
                break;
            case 20:
                if (questActionIndex > 0)
                {
                    GameManager.Inst.GoldChange(+30);
                }
                break;
        }
    }
    public void QuestCountCheck()
    {
        if (questValue > questCount)
        {
            questCount++;
        }
        if (questCount >= questValue)
        {
            questCheck = true;
            questActionIndex++;
        }
    }
}
