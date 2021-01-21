using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    //Parameters


    //Memeber Variables
    int currentScore = 0;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText = GetComponent<Text>();
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int score)
    {
        currentScore = currentScore + score;
        scoreText.text = currentScore.ToString();
    }
}
