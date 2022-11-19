using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dirtyModel;
    [SerializeField] private GameObject cleanModel;
    [SerializeField] private healthbar bar;
    private health health; //TODO Change this to a Health object
    //private bool canTakeDamage;
    [SerializeField] private Canvas healthBarCanvas;
    private GameObject propManager;
    private bool isClean;

    void Start()
    {
        //canTakeDamage = true;
        health = GetComponentInChildren<health>();
        //dirtyMesh = gameObject.GetComponent<MeshRenderer>();
        //cleanMesh = cleanModel.GetComponent<MeshRenderer>();
        //healthBarCanvas = gameObject.GetComponentInChildren<Canvas>();
        dirtyModel.SetActive(true);
        cleanModel.SetActive(false);
        healthBarCanvas.enabled = true;
        isClean = false;
        health.OnDeath += MakeClean;
    }

    private void Awake()
    {
        propManager = GameObject.FindGameObjectWithTag("PropManager");
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void MakeClean()
    {
        if (!isClean)
        {
            propManager.GetComponent<PropManager>().IncreaseCount();
            dirtyModel.SetActive(false);
            cleanModel.SetActive(true);
            healthBarCanvas.enabled = false;
            isClean = true;
        }
    }


    private void UpdateHealthBar()
    {
        bar.sethealth(health.GetMaxHealth(), health.GetCurrentHealth());
    }
}
