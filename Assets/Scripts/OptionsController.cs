using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] Slider difficultySlider;
    float defaultDifficulty = 1;

    [SerializeField] TextMeshProUGUI masterVolumeText;
    [SerializeField] Slider masterVolumeSlider;
    float defaultVolume = 0.2f;

    [SerializeField] TextMeshProUGUI soundEffectsText;
    [SerializeField] Slider soundEffectsSlider;
    float defaultEffects = 0.8f;

    MusicPlayer musicPlayer;

    private void Awake()
    {
        SetDefaultSettings();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
        masterVolumeSlider.value = PlayerPrefsController.GetMasterVolume();
        soundEffectsSlider.value = PlayerPrefsController.GetEffectsVolume();
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

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        musicPlayer.SetMusicVolume(masterVolumeSlider.value);
    }

    private void Update()
    {
        musicPlayer.SetMusicVolume(masterVolumeSlider.value);
    }

    public void ChangeVolumeText()
    {
        masterVolumeText.text = Mathf.RoundToInt(masterVolumeSlider.value * 100).ToString() + "%";
    }

    public void ChangeEffectsText()
    {
        soundEffectsText.text = Mathf.RoundToInt(soundEffectsSlider.value * 100).ToString() + "%";
    }


    public void SetDefaultSettings()
    {
        difficultySlider.value = defaultDifficulty;
        masterVolumeSlider.value = defaultVolume;
        soundEffectsSlider.value = defaultEffects;
    }

    public void LoadMainMenu()
    {
        PlayerPrefsController.SetDifficulty(difficultySlider.value);
        PlayerPrefsController.SetMasterVolume(masterVolumeSlider.value);
        PlayerPrefsController.SetEffectsVolume(soundEffectsSlider.value);
        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }
}
