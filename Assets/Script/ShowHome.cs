using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowHome : MonoBehaviour
{
    public GameObject WindowHome;
    //public Scene Home;

    private void OnEnable()
    {
        //SceneManager.MoveGameObjectToScene(WindowHome, Home);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            WindowHome.SetActive(true);
        }
    }

    public  void Exit()
    {
        WindowHome.SetActive(false);
    }
}
