using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject cleanModel;
    [SerializeField] private healthbar bar;
    private health health; //TODO Change this to a Health object
    //private bool canTakeDamage;
    [SerializeField] private MeshRenderer dirtyMesh;
    [SerializeField] private MeshRenderer cleanMesh;
    [SerializeField] private Canvas healthBarCanvas;
    private GameObject propManager;
    private bool isClean;

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
        isClean = false;
    }

    private void Awake()
    {
        health.OnDeath += MakeClean;
        propManager = GameObject.FindGameObjectWithTag("PropManager");
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
        if (!isClean)
        {
            propManager.GetComponent<PropManager>().IncreaseCount();
            dirtyMesh.enabled = false;
            cleanMesh.enabled = true;
            healthBarCanvas.enabled = false;
            isClean = true;
        }
    }


    private void UpdateHealthBar()
    {
        bar.sethealth(health.GetMaxHealth(), health.GetCurrentHealth());
    }
}
