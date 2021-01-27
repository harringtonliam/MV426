using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }



    public void DisplayScore(int score)
    {
        Text scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }
}
