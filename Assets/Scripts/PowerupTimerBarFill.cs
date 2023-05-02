using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupTimerBarFill : MonoBehaviour
{
    [SerializeField] private float timerDuration = 15f;
    [SerializeField] private Image timerBarImage;

    private void Update()
    {
        timerBarImage.fillAmount -= Time.deltaTime / timerDuration;
    }
}
