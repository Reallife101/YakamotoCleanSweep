using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class playerhealth : health
{
    
    private bool ifinvincible = false;
    [SerializeField] float invinicible_time = 3.0f;
    [SerializeField] private UpdateUI playerHealthUI;

    //FOR TESTING ONLY
    void Update(){

        if (currentHealth <= 0 && isAlive)
        {
            Debug.Log("REPORTDEATH");
            ReportDeath();
           
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
           TakeDamage(1);
        }
        
   }

    

    public void TakeDamage(int healthPTS)
    {
        if (!ifinvincible){
            currentHealth -= healthPTS;
            Debug.Log("damage down 1");
            StartCoroutine(InvincibilityElapse());
            playerHealthUI.setHealth(currentHealth);
        }
    }

    private IEnumerator InvincibilityElapse()
    {
        ifinvincible = true;
        yield return new WaitForSeconds(invinicible_time);
        ifinvincible = false;
        
    }


   

   
}