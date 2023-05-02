using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using UnityEngine.Networking;

public class GameDataLog : MonoBehaviour
{
    public class LoggingDeathResults
    {
        public string timestamp;
        public string levelID;
    }


    public void LogDeathResultData(LoggingDeathResults log)
    {
        StartCoroutine(LogDeaths(log));
    }

    IEnumerator LogDeaths(LoggingDeathResults log) {
        WWWForm form = new WWWForm();

        form.AddField("timestamp", log.timestamp);
        form.AddField("levelID", log.levelID);

        UnityWebRequest w = UnityWebRequest.Post("/SpaceOddity", form);
        w.SendWebRequest();

        while (!w.isDone)
        {
            yield return w;
        }
        
        w.Dispose();
    }
}
