using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class ProgressBar : MonoBehaviour
{
    public GameObject bar;
    public int time;

    // Start is called before the first frame update
    void Start()
    {
        AnimateBar();
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }

    
}
