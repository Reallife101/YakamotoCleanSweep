using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
   
    
    [SerializeField] private Image foreground;
    public void sethealth(int maxhealth, int health){
        foreground.fillAmount = ((float)maxhealth-(float)health)/(float)maxhealth;
        Debug.Log(health);
    }
}
