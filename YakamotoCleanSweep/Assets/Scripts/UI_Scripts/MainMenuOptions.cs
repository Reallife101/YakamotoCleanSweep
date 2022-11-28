using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuOptions : MonoBehaviour
{
    //this script holds all the functionality that can exist without maincomponents
    //mostly it just sets player prefs instead of calling things to update

    // Update is called once per frame
    [SerializeField] TextMeshProUGUI sprintToggleText;
    [SerializeField] TextMeshProUGUI characterText;
    private bool toggleOn;

    private void Start() {
        Debug.Log(PlayerPrefs.GetInt("sprintToggle"));
        toggleOn = (PlayerPrefs.GetInt("sprintToggle", 0) == 1);
        if (PlayerPrefs.GetInt("sprintToggle", 0) == 1) {
            sprintToggleText.text = "Toggle";
        }
        else {
            sprintToggleText.text = "Hold";
        }
        if (PlayerPrefs.GetString("character", "") == "") {
            characterText.text = "Butler";
        }
        else if (PlayerPrefs.GetString("character") == "butler") {
            characterText.text = "Butler";
        }
        else {
            characterText.text = "Maid";
        }
    }

    public void swapToggleSprint() {
        if (toggleOn){
            PlayerPrefs.SetInt("sprintToggle", 0);
            toggleOn = false;
            sprintToggleText.text = "Hold";
        }
        else {
            PlayerPrefs.SetInt("sprintToggle", 1);
            toggleOn = true;
        sprintToggleText.text = "Toggle";
        }
        Debug.Log(PlayerPrefs.GetInt("sprintToggle"));
    }

    public void swapCharacter() {
        if (characterText.text == "Butler"){
            PlayerPrefs.SetString("character", "maid");
            characterText.text = "Maid";
        }
        else {
            PlayerPrefs.SetString("character", "butler");
            characterText.text = "Butler";
        }
        Debug.Log(PlayerPrefs.GetString("character"));
    }

    public void setSensitivityPref(float sens) {
        PlayerPrefs.SetFloat("sensitivity", sens);
    }

    
}
