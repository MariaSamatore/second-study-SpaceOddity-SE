using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    private string[] sentences;
    
    [SerializeField] private GameObject[] characterImage;
    [SerializeField] private float typingSpeed;
    private AudioSource sfx;
    private int index;
    
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject lastButton;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject keyboardImage;

    [SerializeField] private string levelNameToLoad;


    void Start()
    {
        sentences = new string[] {"Major Tom: Hello "+ PlayerPrefs.GetString("playerNamePref") + "! This is Major Tom. I\'m so excited you volunteered to take on this quest to beat Ziggy Stardust. Ziggy and I were the best pilots in our base. One day, ground control gave us a mission to travel to the alien territory to collect space loot and bring them to the base.",
        "Major Tom: Ziggy betrayed me. He sabotaged my spaceship. Not having my full attack power, the aliens killed me in the first stage of the mission. However, I came back as a ghost to guide other courageous adventurers, such as yourself, to avenge me and beat Ziggy Stardust\'s record.",
        "Major Tom: Lady M was my best pilot so far. She almost beat Ziggy, but was killed only a few seconds before the end of the final stage.",
        "Lady M: I am a ghost now, but the memory is still fresh and painful! My spaceship was behaving strangely when it happened.",
        "Major Tom: In this quest, you will face threats like hostile ships and asteroids. Prove that you are the best pilot out there by eliminating the most threats, collecting coins and surviving the enemy ambush. I\'m counting on you, "+ PlayerPrefs.GetString("playerNamePref") + "!",
        "Lady M: Destroy the asteroids if you can. The big asteroids carry power-ups which will help you survive and progress. Also, the coins may disappear in space if you do not collect them quickly.",
        "Major Tom: Move around using the WASD or the arrow keys. Shooting is automatic. You can pause the game at any time by pressing the ESC button.",
        "Major Tom: At some point in the first and third stages of the mission, you will see the ghost of my spaceship and Lady M\'s respectively a few moments before we were killed.",
        "Lady M: Good Luck, " + PlayerPrefs.GetString("playerNamePref") + "!" };


        sfx = GetComponent<AudioSource>();
        StartCoroutine(PopupDialog());
    }

    IEnumerator PopupDialog()
    {
        yield return new WaitForSeconds(1);
        dialogPanel.SetActive(true);
        characterImage[0].SetActive(true);
        StartCoroutine(Type());
        sfx.Play();
    }

    void Update()
    {
        ////// Hidden skip button for developer
        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    StopAllCoroutines();
        //    EndDialog();
        //}

        //User input control
        if (Input.GetKeyDown(KeyCode.Space) && continueButton.activeInHierarchy == true) NextSentence();
        if (Input.GetKeyDown(KeyCode.Space) && lastButton.activeInHierarchy == true) EndDialog();
        if (Input.GetKeyDown(KeyCode.Return) && startGameButton.activeInHierarchy == true) StartGame();


            if (textDisplay.text == sentences[index] && index != sentences.Length - 1)
        {
            continueButton.SetActive(true);
        }

        if (textDisplay.text == sentences[index] && index == sentences.Length - 1)
        {
            lastButton.SetActive(true);
        }

        // Keyboard picture
        if (textDisplay.text == sentences[6]) keyboardImage.SetActive(true);
        if (textDisplay.text != sentences[6]) keyboardImage.SetActive(false);     
    }


    IEnumerator Type()
    {
        continueButton.SetActive(false);
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        sfx.Play();
        if( index < sentences.Length - 1)
        {
            characterImage[index].SetActive(false);
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
            characterImage[index].SetActive(true);
        }
        
        else //for the last sentence
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialogPanel.SetActive(false);
            characterImage[index].SetActive(false);
        }
    }

    public void EndDialog()
    {
        textDisplay.text = "";
        lastButton.SetActive(false);
        dialogPanel.SetActive(false);
        characterImage[index].SetActive(false);
        StartCoroutine(ActivateStartButton());
    }

    public IEnumerator ActivateStartButton()
    {
        yield return new WaitForSeconds(1);
        startGameButton.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelNameToLoad);
    }

}
