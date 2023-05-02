using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class InputWindow : MonoBehaviour
{
    [HideInInspector] public InputWindow login; //referencing this class

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject OKButton;
    [HideInInspector] public string playerName;
    [SerializeField] private GameObject warningText;

    private void Awake()
    {
        if (login == null)
        {
            login = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerName != "")
        {
            warningText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Return) && OKButton.activeInHierarchy == true) AcceptInput();
        if (Input.GetKeyDown(KeyCode.Tab)) inputField.Select();
    }

    public void AcceptInput()
    {
        playerName = inputField.text;
        if (playerName == "")
        {
            warningText.SetActive(true);
        }
        else
        {
            if (playerName.Length <= 15)
            {
                PlayerPrefs.SetString("playerNamePref", playerName);
                PlayerPrefs.Save();
                SceneManager.LoadSceneAsync("Landing");
            }
        }
    }
}
