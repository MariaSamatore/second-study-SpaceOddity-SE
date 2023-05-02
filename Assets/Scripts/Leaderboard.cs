using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    //This script updates two UI texts on the leaderboard : score text and name text 
    private int score;
    private int[] fakeScores = new int[7];

    [SerializeField] private Text mainScoreText;
    [SerializeField] private Text score2Text;
    [SerializeField] private Text score3Text;
    [SerializeField] private Text score4Text;
    [SerializeField] private Text nameField; 

    string playerName; 

    [SerializeField] private GameObject NextLevelButton;
    [SerializeField] private string levelNameToLoad;

    private void Start()
    {
        score = GameManager.instance.ReturnScoreToLeaderboard();
        playerName = GameManager.instance.username;
        nameField.text = playerName; 
        mainScoreText.text = score.ToString();

        //Creating a list of all the other scores which are all less than the player's score
        SetOtherScores(fakeScores);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) NextLevel();
    }

    private void SetOtherScores(int[] fakeScores)
    {
        for (int i = 0; i < 3; i++)
        {
            int newFakeScore = score - UnityEngine.Random.Range(10, 50);
            if (newFakeScore >= 0) fakeScores[i] = newFakeScore;
            else fakeScores[i] = 0;
        }

        //sorting the fake scores
        Array.Sort(fakeScores);
        Array.Reverse(fakeScores);

        score2Text.text = fakeScores[0].ToString();
        score3Text.text = fakeScores[1].ToString();
        if(score4Text)
        {
            score4Text.text = fakeScores[2].ToString();
        }
    }

    public void NextLevel()
    {
        if(levelNameToLoad != null)
        {
            SceneManager.LoadScene(levelNameToLoad);
        }
    }

}
