using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public string SceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    public void Move()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Warp(string _scenename)
    {
        SceneManager.LoadScene(_scenename);
    }
}
