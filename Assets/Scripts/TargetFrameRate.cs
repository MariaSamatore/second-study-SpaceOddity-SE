using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int targetFrameRate = 60;

    private void Awake()
    {
        //Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }


    void Update()
    {
        if (targetFrameRate != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
