using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetData : MonoBehaviour
{
    public string url = "https://full.tvetxr.ga/api/scoreBoard";

    private void Start()
    {
        StartCoroutine(GetData_Coroutine());
    }
    IEnumerator GetData_Coroutine()
    {
        Debug.Log("Loading.....");

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
