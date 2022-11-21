using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class playerhealth : health
{
   private bool ifinvincible = false;
   [SerializeField] float invinicible_time = 3.0f;

   //FOR TESTING ONLY
    void Update(){

        if (Input.GetKeyDown(KeyCode.P))
        {
           TakeDamage(1);
        }
        
   }

    

    public new void TakeDamage(int healthPTS)
    {
        if (!ifinvincible){
        currentHealth -= healthPTS;
        CheckHealth();
        Debug.Log("damage down 1");
        StartCoroutine(InvincibilityElapse());
    }
    }

      private IEnumerator InvincibilityElapse()
    {
        ifinvincible = true;
        yield return new WaitForSeconds(invinicible_time);
        ifinvincible = false;
        
    }


   

   
}