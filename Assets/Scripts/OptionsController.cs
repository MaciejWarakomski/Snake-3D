using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] Slider difficultySlider;

    private void Awake()
    {
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
        PlayerPrefsController.SetDifficulty(difficultySlider.value);
    }

    public void ChangeDifficultyText()
    {
        switch (difficultySlider.value)
        {
            case 0:
                difficultyText.text = "EASY";
                PlayerPrefsController.SetDifficulty(difficultySlider.value);
                break;
            case 1:
                difficultyText.text = "MEDIUM";
                PlayerPrefsController.SetDifficulty(difficultySlider.value);
                break;
            case 2:
                difficultyText.text = "HARD";
                PlayerPrefsController.SetDifficulty(difficultySlider.value);
                break;
            default:
                break;
        }
    }
}
