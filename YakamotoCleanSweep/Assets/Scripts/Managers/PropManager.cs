using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{

    // TODO: Add door object
    [SerializeField] private GameObject elevator_door; 


    private int PROPS_CLEANED_COUNT = 0;

    private static PropManager prop_manager;

    [SerializeField]
    private static GameObject[] props;  

    // Start is called before the first frame update
    void Awake()
    {
        props = new GameObject[0];
        
        if (prop_manager == null)
        {
            prop_manager = this;
            DontDestroyOnLoad(prop_manager);
        }

        props = GameObject.FindGameObjectsWithTag("Prop");

        PROPS_CLEANED_COUNT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCount(); 
    }

    private bool CheckCount()
    {

        if (PROPS_CLEANED_COUNT == props.Length)
        {
            return true;
        }
        else
        {
            return false;
        }    
    }

    public void IncreaseCount()
    {
        PROPS_CLEANED_COUNT += 1;
    } 

    public int RemainingProps()
    {
        return props.Length - PROPS_CLEANED_COUNT;
    }
}
