using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    float playerScore;

    private void Awake()
    {
        int numScoreHandlers = FindObjectsOfType<ScoreHandler>().Length;
        if (numScoreHandlers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetScore(float score)
    {
        playerScore = score;
    }

    public float GetScore()
    {
        return playerScore;
    }
}
