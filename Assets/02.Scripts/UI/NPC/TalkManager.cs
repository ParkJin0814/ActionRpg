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
        //일반대화
        talkData.Add(1000, new string[] { "안녕?", "사고싶은 물건이 있니?" });
        talkData.Add(2000, new string[] { "반가워" });
        //퀘스트대화
        talkData.Add(10 + 2000, new string[] 
        { "반가워", "혹시 바쁘지않으면 내 부탁좀 들어줄레?", "여우좀 잡아줘!" });
        talkData.Add(20 + 2000, new string[] 
        { "잊어버린거 있는거야?", "빨리 여우좀 잡아줘!" });
        talkData.Add(20 + 1 + 2000, new string[] 
        { "벌써 잡아온거야?", "고마워 이건 내 성의니까 받아줘!", "30골드야!" });
        talkData.Add(30 + 2000, new string[] 
        { "안녕~", "이번에도 혹시 내 부탁 좀 들어줄 수 있을까?",
        "공원에서 이상한 소리가 나고 있어" ,"확인 좀 해줄 수 있어?"});
        talkData.Add(40 + 2000, new string[] { "확인해봤어?" });
        talkData.Add(40 + 1 + 2000, new string[] 
        { "공원에 그런 존재가 있었다니...", "넌 우리 마을에 영웅이야 고마워" });
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
