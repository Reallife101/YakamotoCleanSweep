using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropCounter : MonoBehaviour
{
    [SerializeField] private string promptText;
    private PropManager propManager;
    private TMPro.TextMeshProUGUI propText;


    // Start is called before the first frame update
    void Start()
    {
        propManager = FindObjectOfType<PropManager>();
        propText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        propText.text = promptText + propManager.RemainingProps().ToString();
    }
}
