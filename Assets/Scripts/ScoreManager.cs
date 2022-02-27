using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public JsonController JC;
    public Text timer;
    public Text boxCount;
    public int totalBox = 6;
    public GameObject interactable;
    public GameObject canvasTimer;
    public GameObject canvasScore;
    public float timeRemaining = 60;

    public Text nameScore;
    public Text rankScore;
    public Text boxScore;
    public Text timerScore;

    string playername;
    string score;
    int box;
    float _timerScore;

    bool timerStop;
    bool boxLifted;

    Animator animTable;
    Animator animScoreCanvas;


    private void Start()
    {
        boxLifted = false;
        timerStop = true;
        totalBox = totalBox - 1;
        box = 0;
        animTable = interactable.GetComponent<Animator>();
        animScoreCanvas = canvasScore.GetComponent<Animator>();
    }

    private void Update()
    {
        if(timerStop == false && boxLifted == true)
        {
            if (timeRemaining > 0)
            {
                DisplayTime(timeRemaining);
            }
            else
            {
                timer.text = "TIMES UP!";
                timer.color = Color.red; 
                CalculateScore();
                SaveScore();
            }

            if (box == totalBox)
            {
                timer.text = "GOOD JOB!";
                timer.color = Color.green;            
                CalculateScore();
                SaveScore();
            }
        }
    }

    public void AddScore()
    {
        if(boxLifted == false)
        {
            boxLifted = true;
        }

        box++;
        boxCount.text = "Box stacked: " + box.ToString();
        Debug.Log(box);
    }

    public void ReduceScore()
    {
        box--;
        boxCount.text = "Box stacked: " + box.ToString();
        Debug.Log(box);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeRemaining -= Time.deltaTime;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}", seconds);
    }

    public void SaveName(string name)
    {
        playername = name;
        animTable.Play("Appear");
        timerStop = false;
        canvasTimer.SetActive(true);           
    }

    public void SaveScore()
    {
        timerStop = true;
        animTable.Play("Dissappear");

        animScoreCanvas.Play("Appear");
        canvasTimer.SetActive(false);

        JC.ParseData(playername, score, box, _timerScore);
    }

    void CalculateScore()
    {
        _timerScore = Mathf.Round(timeRemaining * 100f) / 100f; ;
        boxScore.color = Color.green;

        if (_timerScore > 50)
        {
            score = "A";
            rankScore.color = Color.green;
            timerScore.color = Color.green;
        }
        else if(_timerScore > 40 && _timerScore <= 50)
        {
            score = "B";
            rankScore.color = Color.blue;
            timerScore.color = Color.blue;
        }
        else if(_timerScore >30 && _timerScore <= 40)
        {
            score = "C";
            rankScore.color = Color.yellow;
            timerScore.color = Color.yellow;
        }
        else
        {
            score = "D";
            rankScore.color = Color.red;
            timerScore.color = Color.red;

            if (box > 3)
            {
                boxScore.color = Color.blue;
            }
            else if(box > 1 && box <= 3)
            {
                boxScore.color = Color.yellow;
            }
            else if(box == 1)
            {
                boxScore.color = Color.red;
            }
        }

        nameScore.text = playername;
        rankScore.text = score;
        boxScore.text = box.ToString();
        timerScore.text = _timerScore.ToString();
    }
}
