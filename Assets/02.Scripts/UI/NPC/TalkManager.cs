using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //��ȭ�� ���õ� ������ �����Һ���
    Dictionary<int, string[]> talkData;
    //��ȭ�ϰ� �ִ� npc�� ��������
    Dictionary<int, Sprite> portraitDate;
    //portraitData�� �ʱ�ȭ ���� Sprite�� ������ ����
    public Sprite[] portraitSprite;
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        portraitDate = new Dictionary<int, Sprite>();
        MakeData();
    }
    void MakeData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?", "�� ���� ó�� �Ա���" });
        talkData.Add(2000, new string[] { "ó�� ���� ���ε�", "������??" });

        portraitDate.Add(1000, portraitSprite[0]);
        portraitDate.Add(2000, portraitSprite[1]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetSprite(int id)
    {
        return portraitDate[id];
    }
}

