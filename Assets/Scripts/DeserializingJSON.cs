using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DeserializingJSON : MonoBehaviour
{
    public string url = "https://tvetxr.my/api/scoreBoard";

    public Transform rowParent;
    public GameObject rowPrefab;
    public Text yourRankText;

    string playerName;
    ScoreDatas [] scoreDatas;

    public void PlayerName(string name)
    {
        playerName = name;
    }

    public void ShowLeaderboard()
    {
        StartCoroutine(GetData_Coroutine());
    }

    IEnumerator GetData_Coroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            { 
                string jsonString = fixJson(request.downloadHandler.text);
                scoreDatas = JsonHelper.FromJson<ScoreDatas>(jsonString);
                CreateScore();
                FindRank();
            }
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    void CreateScore()
    {
        int i = 1;
        string rankString;

        foreach (var item in scoreDatas)
        {
            if (i < 11)
            {
                GameObject newGo = Instantiate(rowPrefab, rowParent);
                Text[] texts = newGo.GetComponentsInChildren<Text>();

                switch (i)
                {
                    default:
                        rankString = i.ToString() + "TH"; break;

                    case 1: rankString = "1ST"; break;
                    case 2: rankString = "2ND"; break;
                    case 3: rankString = "3RD"; break;
                }

                texts[0].text = rankString;
                texts[1].text = item.name;
                texts[2].text = item.score;
                texts[3].text = item.time_completion;

                i++;
            }
            else
            {
                return;
            }
            
        }
    }

    void FindRank()
    {
        for (int i = 0; i < 10; i++)
        {
            if (scoreDatas[i].name == playerName)
            {
                yourRankText.text = "YOUR RANK: " + (i + 1).ToString() + "!";
                yourRankText.color = Color.green;
                break;
            }
            else
            {
                yourRankText.text = "YOUR RANK: UNRANKED";
                yourRankText.color = Color.red;
            }
        }

    }
}
