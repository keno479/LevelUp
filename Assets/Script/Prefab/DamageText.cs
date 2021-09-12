using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float speed = 3.0f;
    void Start()
    {
        
        Destroy(gameObject, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(0, speed, 0);
        speed *= 0.98f;
    }
}
