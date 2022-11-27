using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisualManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] UpdateUI ui;
    // Start is called before the first frame update
    private void Start() {
        if (PlayerPrefs.GetString("character", "") == "") {
            textMesh.text = "Butler";
        }
        else if (PlayerPrefs.GetString("character") == "butler") {
            textMesh.text = "Butler";
        }
        else {
            textMesh.text = "Maid";
        }
    }

    public void swapCharacter() {
        if (textMesh.text == "Butler"){
            PlayerPrefs.SetString("character", "Maid");
            textMesh.text = "Maid";
        }
        else {
            PlayerPrefs.SetString("character", "Butler");
            textMesh.text = "Butler";
        }
        ui.updateCharacter();
        Debug.Log(PlayerPrefs.GetString("character"));
    }
}
