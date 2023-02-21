using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    //대화에 관련된 정보를 저장할변수
    Dictionary<int, string[]> talkData;
    //대화하고 있는 npc에 사진저장
    Dictionary<int, Sprite> portraitDate;
    //portraitData를 초기화 해줄 Sprite를 저장할 변수
    public Sprite[] portraitSprite;
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        portraitDate = new Dictionary<int, Sprite>();
        MakeData();
    }
    void MakeData()
    {
        talkData.Add(1000, new string[] { "안녕?", "이 곳에 처음 왔구나" });
        talkData.Add(2000, new string[] { "처음 보는 얼굴인데", "누구야??" });

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

