using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBofSignal : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) EndGame();
    }
    public void EndGame() 
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBGL
            Application.ExternalCall("EndGame");
    #else
            Application.Quit();
    #endif
    }
}
