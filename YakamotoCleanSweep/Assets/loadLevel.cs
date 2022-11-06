using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class loadLevel : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject endScreen;
    [SerializeField] TMP_Text timer;
    [SerializeField] Timer tm;
    [SerializeField] PlayerLook pl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //If its the player, show the end screen and pause the game
            endScreen.SetActive(true);
            timer.text = tm.getCurrentTime();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pl.allowLooking = false;

        }
    }

    public void loadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
