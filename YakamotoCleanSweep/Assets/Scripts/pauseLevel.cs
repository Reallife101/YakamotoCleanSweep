using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class pauseLevel : MonoBehaviour
{
    [SerializeField] GameObject pauseMen;
    [SerializeField] PlayerLook pl;
    

    // Update is called once per frame
    public bool isPaused() {
        return pauseMen.activeSelf;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
            pauseMen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pl.allowLooking = false;
        }
        else
        {
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
