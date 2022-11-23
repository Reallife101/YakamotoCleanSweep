using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class startLevel : MonoBehaviour
{
    [SerializeField] GameObject door;
    
    private Timer tm;
    private TextMeshProUGUI textMesh;
    void Start()
    {
        tm = GetComponent<Timer>();
        tm.enabled = false;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = "3";

        StartCoroutine(Countdown(1, "2", false));
        StartCoroutine(Countdown(2, "1", false));
        StartCoroutine(Countdown(3, "Go!", true));
    }


    IEnumerator Countdown(int seconds, string tx, bool b)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        textMesh.text = tx;
        tm.enabled = b;
        door.SetActive(!b);
    }
}
