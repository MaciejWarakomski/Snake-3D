using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float playerScore;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        int numGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numGameManagers > 1)
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
