using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class loadLevel : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject gold;
    [SerializeField] GameObject silver;
    [SerializeField] GameObject bronze;
    [SerializeField] TMP_Text timer;
    [SerializeField] Timer tm;
    [SerializeField] PlayerLook pl;
    [SerializeField] int goldTime = 20;
    [SerializeField] int silverTime = 45;
    [SerializeField] pauseLevel pause;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float myTime = tm.getCurrentTimeAsNum();
            //If its the player, show the end screen and pause the game
            endScreen.SetActive(true);
            timer.text = tm.getCurrentTime();
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pl.allowLooking = false;
            pause.enablePause = false;
            if (myTime < goldTime) {
                gold.SetActive(true);
            }
            else if (myTime < silverTime) {
                silver.SetActive(true);
            }
            else {
                bronze.SetActive(true);
            }
        }
    }

    public void loadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
