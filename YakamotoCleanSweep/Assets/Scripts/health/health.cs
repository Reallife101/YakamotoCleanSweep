using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    private int currentHealth;
    private bool isAlive;

    public event Action OnDeath;

    void Start()
    {
        currentHealth = MaxHealth;
        isAlive = true;
    }

    private void Update()
    {
        if (currentHealth <= 0 && isAlive)
        {
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