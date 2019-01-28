using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MusicManager : MonoBehaviour
{
    #region Fields
    private float normalMusicVolume = 0f;
    private float knightMusicVolume = 0f;

    private AudioSource[] audioSources = null;
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        Assert.IsNotNull(audioSources);

        audioSources[0].Play();

        normalMusicVolume = audioSources[0].volume;
        knightMusicVolume = audioSources[1].volume;
    }

    // Update is called once per frame
    private void Update()
    {
    }
    #endregion

    #region Public Methods
    public void ChangeMusic(GameManager.WorldState worldState)
    {
        Debug.Log("Changing Music.");

        if (worldState == GameManager.WorldState.Normal)
        {
            audioSources[0].volume = normalMusicVolume;
            audioSources[1].volume = 0f;
        }
        if (worldState == GameManager.WorldState.Knight)
        {
            audioSources[0].volume = 0f;
            audioSources[1].volume = knightMusicVolume;

            audioSources[1].Play();
        }
        //musicSource.clip = nextMusicClip;
        //musicSource.Play();
    }
    #endregion
}