using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHome : MonoBehaviour
{
    public GameObject WindowHome;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            WindowHome.SetActive(true);
        }
    }
}
