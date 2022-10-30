using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : PropManager
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cleanModel;
    [SerializeField] private healthbar bar;
    private health health; //TODO Change this to a Health object
    //private bool canTakeDamage;
    [SerializeField] private MeshRenderer dirtyMesh;
    [SerializeField] private MeshRenderer cleanMesh;
    [SerializeField] private Canvas healthBarCanvas;

    void Start()
    {
        //canTakeDamage = true;
        health = GetComponent<health>();
        //dirtyMesh = gameObject.GetComponent<MeshRenderer>();
        //cleanMesh = cleanModel.GetComponent<MeshRenderer>();
        //healthBarCanvas = gameObject.GetComponentInChildren<Canvas>();
        dirtyMesh.enabled = true;
        cleanMesh.enabled = false;
        healthBarCanvas.enabled = true;
    }

    public Prop()
    {
        health.OnDeath += MakeClean;
    }

    void Update()
    {
        UpdateHealthBar();
        if (Input.GetMouseButtonDown(0))
        {
            health.TakeDamage(1);
        }
    }

    private void MakeClean()
    {
        dirtyMesh.enabled = false;
        cleanMesh.enabled = true;
        healthBarCanvas.enabled = false;
        base.IncreaseCount();
    }


    private void UpdateHealthBar()
    {
        bar.sethealth(health.GetMaxHealth(), health.GetCurrentHealth());
    }
}
