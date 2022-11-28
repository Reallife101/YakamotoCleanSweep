using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider sensitivity;
    [SerializeField] private TextMeshProUGUI sliderText;
    // Start is called before the first frame update
    private void Start()
    {
        sensitivity.value = PlayerPrefs.GetFloat("sensitivity", .01f);
        this.setSliderTextValue(PlayerPrefs.GetFloat("sensitivity", .01f));
    }

    public void setSliderTextValue(float value) {
        sliderText.text = "Sensistivity : " + value.ToString("0.000");
    }

    public void toMainMenu() {
        SceneManager.LoadScene(0);
    }
}
