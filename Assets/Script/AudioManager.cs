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
        SetBGM();
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeBGM()
    {
        if (SceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            SceneIndex = SceneManager.GetActiveScene().buildIndex;
            audios.clip = clips[SceneIndex];
        }
        SetBGM();
    }

    public void SetBGM()
    {
        if (TitleData.Instance.Config.GetInt(Define.KeyBGMOn) == 0)
        {
            audios.Play();
        }
        else
        {
            audios.Stop();
        }
    }
}