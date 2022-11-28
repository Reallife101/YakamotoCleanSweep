using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private GameObject main;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;

    [SerializeField] private GameObject characterSelect;

    [SerializeField] private int maidLevel = 1;
    [SerializeField] private int butlerLevel = 2;

    private void Start()
    {
        showMain();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        Debug.Log(PlayerPrefs.GetString("character", ""));
        if (PlayerPrefs.GetString("character", "") == "maid")
        {
            SceneManager.LoadScene(maidLevel);
        }
        else if (PlayerPrefs.GetString("character", "") == "butler")
        {
            SceneManager.LoadScene(butlerLevel);
        }
        else {
            this.openCharacterSelect();
        }
    }

    private void openCharacterSelect() {
        main.SetActive(false);
        characterSelect.SetActive(true);
    }

    public void setCharacter(string c) {
        PlayerPrefs.SetString("character", c);
    }

    public void showCredits()
    {
        credits.SetActive(true);
        main.SetActive(false);
        howToPlay.SetActive(false);
    }
    public void showMain()
    {
        credits.SetActive(false);
        main.SetActive(true);
        howToPlay.SetActive(false);
    }

    public void showHowTo()
    {
        credits.SetActive(false);
        main.SetActive(false);
        howToPlay.SetActive(true);
    }


}
