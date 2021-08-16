using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageView : MonoBehaviour
{
    
    void Start()
    {
        transform.Translate(1.5f, 0, 0);
        //Destroy(gameObject, 1.5f);
        transform.DOLocalJump(Vector3.zero, 3, 1, 1.5f).SetRelative(true).
            OnComplete(() => { Destroy(gameObject, 0.2f); });
    }
}
