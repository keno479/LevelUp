using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BtnWarp : MonoBehaviour
{
    public TextMeshProUGUI StageName;
    private string SceneName;

    public void SetWarpTarget(MasterStageParam _param)
    {
        DataStageParam data = DataManager.Instance.datastage.list.Find(p => p.Stage_ID == _param.Stage_ID);

        if (data.is_Open)
        {
            StageName.text = $"{_param.Stage_Name}";
        }
        else
        {
            StageName.text = "???";
        }
        SceneName = _param.Scene_Name;
    }
    
    public void Warp()
    {
        SceneManager.LoadScene(SceneName);
    }
}
