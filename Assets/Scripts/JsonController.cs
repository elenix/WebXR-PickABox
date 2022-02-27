using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
public class JsonController : MonoBehaviour
{
    public string url = "https://tvetxr.my/api/scoreBoard";

    public GameObject leaderboardCanvas;
    public DeserializingJSON DJ;

    ScoreData scoreData;

    public void ParseData(string name, string score, int box, float time)
    {
        scoreData = new ScoreData();

        scoreData.name = name;
        scoreData.score = score;
        scoreData.box = box;
        scoreData.time_completion = time;

        StartCoroutine(PostData_Coroutine());

        leaderboardCanvas.SetActive(true);

        Debug.Log(scoreData.name + "," + scoreData.score + "," + scoreData.box + "," + scoreData.time_completion);     
    }

    IEnumerator PostData_Coroutine()
    {
        WWWForm form = new WWWForm();

        form.AddField("name", scoreData.name);
        form.AddField("score", scoreData.score);
        form.AddField("box", scoreData.box);
        form.AddField("time_completion", scoreData.time_completion.ToString());

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("Response from the server: " + responseText);
                DJ.PlayerName(scoreData.name);
                DJ.ShowLeaderboard();
            }
        }
    }

   
}
