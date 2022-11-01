using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{

    // TODO: Add door object
    [SerializeField] private GameObject elevator_door; 


    private int propsCleanedCount;

    private static PropManager prop_manager;

    [SerializeField]
    private static GameObject[] props;

    private void Start()
    {
        propsCleanedCount = 0;
        //props = new GameObject[0];
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (prop_manager == null)
        {
            prop_manager = this;
            DontDestroyOnLoad(prop_manager);
        }

        props = GameObject.FindGameObjectsWithTag("Prop");
    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
    }

    public bool CheckCount()
    {

        if (propsCleanedCount == props.Length)
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
        propsCleanedCount += 1;
    } 

    public int RemainingProps()
    {
        return props.Length - propsCleanedCount;
    }
}
