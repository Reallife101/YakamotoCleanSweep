using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime;
    
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        currentTime = 50f;
    }

    static string format2Digit (int t) {
        if (t < 10) {
            return "0" + t.ToString();
        }
        else {
            return t.ToString();
        }
        
    }
    static string format(float t) {
        if (t > 60) {
            return (((int)t) / 60).ToString() + ":" + format2Digit((int)t % 60) + "." + format2Digit((int)((t * 100) % 100));
        }
        else {
            return ((int)t).ToString() + "." + format2Digit((int)((t * 100) % 100));
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += 1f * Time.deltaTime;
        textMesh.text = format(currentTime);
    }
}
