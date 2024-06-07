using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System;

[System.Serializable]
public class Rank
{
    public List<RankInfo> rankInfoList = new List<RankInfo>();
}

[System.Serializable]
public class RankInfo : IComparable
{
    public string name;
    public float time;

    public RankInfo(string name, float time)
    {
        this.name = name;
        this.time = time;
    }

    public int CompareTo(object other)
    {
        RankInfo temp = (RankInfo)other;
        return time.CompareTo(temp.time);
    }
}

public class RankingManager : MonoBehaviour
{
    string fileName = "RankingDataJson.JSON";
    public string filePath = "Json/RankingDataJson";

    //Inspecter 창에서 이제 Rank 클래스의 정보 고치기? 접근? 가능
    public Rank rank;

    string rankname;
    float time;

    void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
#elif UNITY_ANDROID
        filePath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/", fileName);
#elif UNITY_IOS
        filePath = Path.Combine(Application.streamingAssetsPath, fileName);
#else
        filePath = null;
#endif
        RankLoadFromJson();
    }

    public void SetRankName(string rankname)
    {
        this.rankname = rankname;
    }

    public void AddRankData()
    {
        time = GameManager.Instance._GameTime;
        RankInfo info = new RankInfo(rankname, time);
        rank.rankInfoList.Add(info);
        RankSaveToJson();
    }

    void RankSaveToJson()
    {
        string jsonData = JsonUtility.ToJson(rank, true);
        File.WriteAllText(filePath, jsonData);
    }

    void RankLoadFromJson()
    {
        string jsonData = File.ReadAllText(filePath);
        rank = JsonUtility.FromJson<Rank>(jsonData);
    }

    public string ConvertRankToString()
    {
        List<RankInfo> sortedList = new List<RankInfo>();
        sortedList = rank.rankInfoList;
        sortedList.Sort((x, y) => x.time.CompareTo(y.time));
        sortedList.Reverse();
        string info = "";
        int i = 0;
        foreach(RankInfo item in sortedList)
        {
            ++i;
            info += i.ToString() + ". " + item.name + " - " + item.time.ToString("F2") + "\n";
        }
        return info;
    }


}
