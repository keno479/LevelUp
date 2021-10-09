using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowWarp : MonoBehaviour
{
    public Transform areaButton;
    private List<MasterStageParam> StageList = new List<MasterStageParam>();

    private void OnEnable()
    {
        StageList=DataManager.Instance.masterstage
        GameObject Warp = Instantiate(PrefabHolder.Instance.BtnWarp, areaButton) as GameObject;
    }
}
