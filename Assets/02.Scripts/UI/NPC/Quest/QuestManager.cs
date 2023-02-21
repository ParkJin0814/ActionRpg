using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Start()
    {
        questList= new Dictionary<int, QuestData>();
        GenerataData();
    }
    void GenerataData()
    {
        questList.Add(10, new QuestData("������ ��ȭ�ϱ�", new int[] { 1000, 2000 }));
        questList.Add(20, new QuestData("���⸦ ã�ƶ�", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����", new int[] { 0 }));
    }
}