using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider foreground;
    [SerializeField] private Image fill;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Gradient grad;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        //canvas = gameObject.GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        canvas.transform.rotation = Quaternion.LookRotation(foreground.transform.position - cam.transform.position);
    }
    
    public void sethealth(int maxhealth, int health){
        fill.color = grad.Evaluate(((float)maxhealth-(float)health)/(float)maxhealth);
        foreground.value = ((float)maxhealth-(float)health)/(float)maxhealth;
    }
}
