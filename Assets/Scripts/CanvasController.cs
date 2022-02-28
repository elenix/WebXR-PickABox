using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasController : MonoBehaviour
{
    public Text nameTitle;
    public ScoreManager SM;
    public GameObject otherPlayerInfo;
    public InputField playerName;

    string _name;

    public void Save()
    {
        if(playerName.textComponent.text.Length > 0)
        {
            nameTitle.text = "Your name: " + playerName.text;
            SM.SaveName(playerName.text);
            gameObject.SetActive(false);
            otherPlayerInfo.SetActive(false);
        }
        else
        {
            _name = GerRandomCharA2Z();
            nameTitle.text = "Your random generated name: " + _name;
            SM.SaveName(_name);
            gameObject.SetActive(false);
            otherPlayerInfo.SetActive(false);
        }
    }

    public string GerRandomCharA2Z()
    {
        string st = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string c = st[Random.Range(0, st.Length)].ToString() + st[Random.Range(0, st.Length)].ToString() + st[Random.Range(0, st.Length)].ToString() + Random.Range(0,25).ToString();

        return c;
    }

    public void KeyboardInput()
    {
        playerName.text = playerName.text + EventSystem.current.currentSelectedGameObject.name;
    }

    public void DeleteString()
    {
        playerName.text = playerName.text.Substring(0, playerName.text.Length - 1);
    }
}
