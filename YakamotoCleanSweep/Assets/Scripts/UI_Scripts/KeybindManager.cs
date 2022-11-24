using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeybindManager : MonoBehaviour
{
    [SerializeField] PlayerMovement pm;
    [SerializeField] TextMeshProUGUI textMesh;
    private bool toggleOn;

    private void Start() {
        Debug.Log(PlayerPrefs.GetInt("sprintToggle"));
        toggleOn = (PlayerPrefs.GetInt("sprintToggle", 0) == 1);
        if (PlayerPrefs.GetInt("sprintToggle", 0) == 1) {
            textMesh.text = "Toggle";
        }
        else {
            textMesh.text = "Hold";
        }
    }

    public void swapToggleSprint() {
        if (toggleOn){
            PlayerPrefs.SetInt("sprintToggle", 0);
            pm.setToggleSprintPM(false);
            toggleOn = false;
            textMesh.text = "Hold";
        }
        else {
            PlayerPrefs.SetInt("sprintToggle", 1);
            pm.setToggleSprintPM(true);
            toggleOn = true;
        textMesh.text = "Toggle";
        }
        Debug.Log(PlayerPrefs.GetInt("sprintToggle"));
    }
}
