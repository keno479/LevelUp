using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] clips;
    private AudioSource audios;
    private int SceneIndex;

    public override void Initialize()
    {
        base.Initialize();
        audios = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (SceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneIndex = SceneManager.GetActiveScene().buildIndex;
            audios.clip = clips[SceneIndex];
            audios.Play();
        }
    }
}