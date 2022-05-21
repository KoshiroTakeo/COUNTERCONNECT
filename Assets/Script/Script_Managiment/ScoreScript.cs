using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI機能を扱うときに追記する

public class ScoreScript : MonoBehaviour
{
    public int score = 0;
    Text scoreText;

    public void AddScore(int EnemyScore)
    {
        this.score += EnemyScore;
    }

    void Start()
    {
        scoreText = GetComponentInChildren <Text>();
    }

    void Update()
    {
        scoreText.GetComponent<Text>().text = "Score:" + score.ToString("D4");
    }
}
