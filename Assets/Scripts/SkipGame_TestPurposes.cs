using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipGame_TestPurposes : MonoBehaviour
{
    [SerializeField] private string levelNameToLoad;
    
    public void SkipGame()
    {
        SceneManager.LoadScene(levelNameToLoad);
    }
}
