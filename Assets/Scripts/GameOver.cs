using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    float score;

    [SerializeField] TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        score = FindObjectOfType<ScoreHandler>().GetScore() * 
            (PlayerPrefsController.GetDifficulty() + 1);
        scoreText.text = score.ToString();
    }
}
