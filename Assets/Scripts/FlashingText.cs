using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FlashingText : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public TextMeshProUGUI text;

    void Update()
    {
        FlashingEffect();
    }

    public void FlashingEffect()
    {
        text.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
    }
}
