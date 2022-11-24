using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Slider sensitivity;
    // Start is called before the first frame update
    private void Start()
    {
        sensitivity.value = PlayerPrefs.GetFloat("sensitivity", .01f);
    }
}
