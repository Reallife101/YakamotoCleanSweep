using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class health : MonoBehaviour
{
    // Start is called before the first frame update
     //private PropManager propManager;
    [SerializeField] private GameObject cleanModel;
    [SerializeField] private int MaxHealth;
    [SerializeField] private healthbar bar;
    private int currentHealth;

    void Start()
    {
        //propManager = GameObject.FindGameObjectWithTag("Prop Manager").GetComponent<PropManager>();      
        currentHealth = MaxHealth;
    }
    void Awake(){
        gameObject.SetActive(true);
        cleanModel.SetActive(false);
    }

    public void TakeDamage(int healthPTS){
       
        if(currentHealth >0){
             currentHealth -= healthPTS;
        bar.sethealth(MaxHealth,currentHealth);
        }
        else{
            ReportDeath();
        }

    }
    private void ReportDeath(){
       
        //propManager.IncreaseCount();
        gameObject.SetActive(false);
        cleanModel.SetActive(true);
        Debug.Log("Dead");

  

    }
    public int GetHealth(){
        return currentHealth;
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
    TakeDamage(1);
}


    
   
}
