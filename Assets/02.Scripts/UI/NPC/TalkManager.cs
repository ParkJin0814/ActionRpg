using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GeneratorData();
    }
    void GeneratorData()
    {
        //�Ϲݴ�ȭ
        talkData.Add(1000, new string[] { "�ȳ�?", "������ ������ �ִ�?" });
        talkData.Add(2000, new string[] { "�ݰ���" });
        //����Ʈ��ȭ
        talkData.Add(10 + 2000, new string[] 
        { "�ݰ���", "Ȥ�� �ٻ��������� �� ��Ź�� ����ٷ�?", "������ �����!" });
        talkData.Add(20 + 2000, new string[] 
        { "�ؾ������ �ִ°ž�?", "���� ������ �����!" });
        talkData.Add(20 + 1 + 2000, new string[] 
        { "���� ��ƿ°ž�?", "���� �̰� �� ���Ǵϱ� �޾���!", "30����!" });
        talkData.Add(30 + 2000, new string[] 
        { "�ȳ�~", "�̹����� Ȥ�� �� ��Ź �� ����� �� ������?",
        "�������� �̻��� �Ҹ��� ���� �־�" ,"Ȯ�� �� ���� �� �־�?"});
        talkData.Add(40 + 2000, new string[] { "Ȯ���غþ�?" });
        talkData.Add(40 + 1 + 2000, new string[] 
        { "������ �׷� ���簡 �־��ٴ�...", "�� �츮 ������ �����̾� ����" });
        portraitData.Add(1000, portraitArr[0]);
        portraitData.Add(2000, portraitArr[1]);
    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex);
            else
                return GetTalk(id - id % 10, talkIndex);
        }
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
