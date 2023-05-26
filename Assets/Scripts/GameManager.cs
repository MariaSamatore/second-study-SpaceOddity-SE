using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public GameObject player;
    [HideInInspector] public string username;
    [HideInInspector] public string participantID;

    //Log
    //public GameDataLog logger;
    //private float curTime = 0;
    public static int deathCountPerLevel = 0;

    //Score variables
    public Text scoreText;
    private int score = 0;

    //Player health variables
    public Text healthText;
    public int maxHealth = 100;
    
    //Health Bar
    public Image healthBar;

    //game over and restart
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool restart;
    public Text gameOverText;
    public Text restartText;

    //Coin variables
    public Text coinText;
    private int coin = 0;
    [SerializeField] AudioClip coinSound;

    private float coin_timer = 0f;


    [SerializeField] private AudioSource audioSource; //This is for the praise


    private void Awake()
    {
        // Singleton. Making sure that only one instance of this script can exist in the game
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full); 
        username = PlayerPrefs.GetString("playerNamePref"); 

        gameOver = false;
        restart = false;
        if (gameOverText && restartText)
        {
            gameOverText.text = "";
            restartText.text = "";
            UpdateScore();
        }
    }
    
    private void Update()
    {

        if (gameOver)
        {
            // If a participant loses 6 times, they'll be kicked out of the game and redirected to the surveys
            if (deathCountPerLevel >= 6)
            {
                SceneManager.LoadScene("Survey Link");
            }

            gameOverText.text = "Game Over";
            restartText.text = "Press 'R' to Restart";
            restart = true;
            Time.timeScale = 0f;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        //increase value of coin timer
        coin_timer += Time.deltaTime;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddPoints(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScore();
    }

    public void UpdateHealth(float health)
    {
        if (healthText) 
        {
            if (health < 0) health = 0;
            
            float percentage = health / maxHealth * 100;
            healthText.text = Mathf.FloorToInt(percentage) + "%";

            //Health bar
            healthBar.fillAmount = health / maxHealth;

            if (health < (maxHealth * 30 / 100)) healthBar.color = Color.red;

            else if (health >= (maxHealth * 30 / 100) && health < (maxHealth * 50 / 100)) healthBar.color = new Color(1, 0.55f, 0, 1);

            else if (health >= (maxHealth * 50 / 100) && health < (maxHealth * 70 / 100)) healthBar.color = Color.yellow;

            else if (health >= (maxHealth * 70 / 100) && health < (maxHealth * 90 / 100)) healthBar.color = new Color(0.66f, 0.94f, 0.2f, 1);

            else if (health >= (maxHealth * 90 / 100)) healthBar.color = new Color(0, 0.8f, 0, 1);
        }
    }

    public void DeathCounterPerLevel()
    {
        deathCountPerLevel += 1;
        Debug.Log(deathCountPerLevel);
    }

    public void AddCoins()
    {
        //use a timer to make sure this function can't be called more than once per frame. This avoids duplicate coins        
        if (coin_timer > 0.01)
        {
            coin_timer = 0f;
            coin++;
            UpdateCoin();
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }
    }
    
    public void UpdateCoin()
    {
        if(coinText)
        {
            coinText.text = "Coins: " + coin;
        }
    }

    public int ReturnScoreToLeaderboard()
    {
        return score;
    }

    public void PlayPraiseSFX(AudioClip praiseClip)
    {
        audioSource.PlayOneShot(praiseClip, 1.0f);
    }
}
