using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int MaxHealth;
    [SerializeField] private int currenthealth;
    [SerializeField] private healthbar bar;
    void Start()
    {
        currenthealth = MaxHealth;
        
    }
    public void takedamage(int pts){
       
        if(currenthealth >0){
             currenthealth -= pts;
        bar.sethealth(MaxHealth,currenthealth);
        }
        else{
            reportdeath();
        }

    }
    public void reportdeath(){

        if(currenthealth == 0){
           Debug.Log("Dead");
        }

    }
   /* 
    void Update(){
        if (Input.GetMouseButton(0))
{    
    takedamage(1);
}
    }
*/

void OnMouseDown(){
    Debug.Log("-1");
    takedamage(1);
}


    
   
}
