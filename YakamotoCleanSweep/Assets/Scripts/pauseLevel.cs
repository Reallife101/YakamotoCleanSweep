using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseLevel : MonoBehaviour
{
    [SerializeField] GameObject pauseMen;
    [SerializeField] GameObject gameUI;
    [SerializeField] PlayerLook pl;

    [HideInInspector] public bool enablePause;
    
    private void Start()
    {
        StartCoroutine(disablePauseFor(3));
    }
    
    IEnumerator disablePauseFor(int seconds)
    {
        enablePause = false;
        yield return new WaitForSeconds(seconds);
        enablePause = true;
    }

    // Update is called once per frame
    public bool isPaused() 
    {
        return pauseMen.activeSelf;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enablePause)
        {
            togglePause();
        }
    }
    
    public void togglePause()
    {
        setPause(!pauseMen.activeSelf);
    }

    public void setPause(bool b)
    {
        if (b)
        {
            gameUI.SetActive(false);
            pauseMen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pl.allowLooking = false;
        }
        else
        {
            gameUI.SetActive(true);
            pauseMen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pl.allowLooking = true;
        }
    }

    public void restartLevel()
    {
        setPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
