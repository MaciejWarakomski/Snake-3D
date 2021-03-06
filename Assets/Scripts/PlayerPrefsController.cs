using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string DIFFICULTY_KEY = "difficulty";
    const string MASTER_VOLUME_KEY = "master volume";
    const string EFFECTS_VOLUME_KEY = "effects volume";

    public static void SetDifficulty(float difficulty)
    {
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
    }

    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat(EFFECTS_VOLUME_KEY, volume);
    }

    public static float GetEffectsVolume()
    {
        return PlayerPrefs.GetFloat(EFFECTS_VOLUME_KEY);
    }
}
