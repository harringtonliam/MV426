using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSession : MonoBehaviour
{

    //Properties
    public int TriggerScore
    {
        get { return triggerScore; }
        set { triggerScore = value;  }
    }

    //member variables
    int currentScore = 0;
    int triggerScore = 0;

    private static GameSession _instance;


    private void Awake()
    {
        //Make a singletom
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreBoard scoreBoard = FindObjectOfType<ScoreBoard>();
        DisplayScore();

    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }

    public void AddScore(int score)
    {
        currentScore = currentScore + score;
        triggerScore = triggerScore + score;
        DisplayScore();

    }

    private void DisplayScore()
    {
        ScoreBoard scoreBoard = FindObjectOfType<ScoreBoard>();
        if (scoreBoard != null)
        {
            scoreBoard.DisplayScore(currentScore);
        }
    }

    public void ResetGame()
    {

        currentScore = 0;
        triggerScore = 0;
    }

    
}
