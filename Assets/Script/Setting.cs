using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting : MonoBehaviour
{
    private bool CameraManual;
    private bool BGM_on_off_bool;
    public TextMeshProUGUI TextCameraMode;
    public TextMeshProUGUI TextBGM_on_off;
    public TextMeshProUGUI TextCurrentFPS;

    private void OnEnable()
    {
        Application.targetFrameRate = 60;
        CameraManual = true;
        BGM_on_off_bool = true;
        SetTextFPS();
        SetCameraMode();
        SetBGM_on_off();
    }

    public void SetTextFPS()
    {
        TextCurrentFPS.text = $"今の設定:{Application.targetFrameRate}FPS";
    }

    public void SetFPSHigh()
    {
        Application.targetFrameRate = 60;
        SetTextFPS();
    }

    public void SetFPSMiddle()
    {
        Application.targetFrameRate = 40;
        SetTextFPS();
    }

    public void SetFPSLow()
    {
        Application.targetFrameRate = 20;
        SetTextFPS();
    }

    public void ChangeCameraMode()
    {
        CameraManual = !CameraManual;
        SetCameraMode();
    }

    public void SetCameraMode()
    {
        if (CameraManual)
        {
            TextCameraMode.text = $"マニュアル";
        }
        else
        {
            TextCameraMode.text = $"オート";
        }
    }

    public void BGM_on_off()
    {
        BGM_on_off_bool = !BGM_on_off_bool;
        SetBGM_on_off();
    }

    public void SetBGM_on_off()
    {
        if (BGM_on_off_bool)
        {
            TextBGM_on_off.text = $"BGMオン";
        }
        else
        {
            TextBGM_on_off.text = $"BGMオフ";
        }
    }
}
