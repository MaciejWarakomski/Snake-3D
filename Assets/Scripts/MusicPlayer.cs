using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> musicsList;
    AudioSource audioSource;
    int lastMusicPlayed;

    private void Awake()
    {
        Singleton();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    private void Singleton()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayNextMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextMusic();
        }
    }

    private void PlayNextMusic()
    {
        lastMusicPlayed = Random.Range(0, musicsList.Count);
        audioSource.clip = musicsList[lastMusicPlayed];
        audioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
