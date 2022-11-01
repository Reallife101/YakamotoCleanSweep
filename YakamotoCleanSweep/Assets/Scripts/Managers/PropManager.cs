using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{

    // TODO: Add door object
    [SerializeField]
    public GameObject elevator_door; 


    private int PROPS_CLEANED_COUNT = 0;

    private static PropManager prop_manager;

    [SerializeField]
    private static GameObject [] props;  

    // Start is called before the first frame update
    void Awake()
    {
        if (prop_manager == null)
        {
            prop_manager = this;
            DontDestroyOnLoad(prop_manager);
        }

        props = GameObject.FindGameObjectsWithTag("Prop");
        Debug.Log(props.Length + " props");

    }

    // Update is called once per frame
    void Update()
    {
        //CheckCount(); 
    }

    public bool CheckCount()
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
        Debug.Log(PROPS_CLEANED_COUNT);
    } 

    public int RemainingProps()
    {
        return props.Length - PROPS_CLEANED_COUNT;
    }
}
