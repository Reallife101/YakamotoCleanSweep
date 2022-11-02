using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image foreground;
    private Canvas canvas;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        //canvas = gameObject.GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        foreground.GetComponentInParent<Canvas>().transform.rotation = Quaternion.LookRotation(foreground.transform.position - cam.transform.position);
    }
    
    public void sethealth(int maxhealth, int health){
        foreground.fillAmount = ((float)maxhealth-(float)health)/(float)maxhealth;
    }
}
