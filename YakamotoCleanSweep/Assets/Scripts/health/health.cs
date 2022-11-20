using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [SerializeField] protected int MaxHealth;
    protected int currentHealth;
    protected bool isAlive;

    public event Action OnDeath;

    void Start()
    {
        currentHealth = MaxHealth;
        isAlive = true;
    }

    protected void CheckHealth()
    {
        if (currentHealth <= 0 && isAlive)
        {
            Debug.Log("REPORTDEATH");
            ReportDeath();
        }
    }

    public void TakeDamage(int healthPTS)
    {

        currentHealth -= healthPTS;

    }

    public void ReportDeath()
    {
        isAlive = false;
        OnDeath?.Invoke();
        currentHealth = 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    /*void Update(){
        if (Input.GetMouseButtonDown(0))
{    
    TakeDamage(1);
}
    }*/


    /*void OnMouseDown(){
        Debug.Log("-1");
        TakeDamage(1);
    }*/
}