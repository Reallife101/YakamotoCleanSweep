using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    [SerializeField]
    public Animator left_door_animator;

    [SerializeField]
    public Animator right_door_animator;

    [SerializeField]
    private bool test_doors = false;

    private int PROPS_CLEANED_COUNT = 0;

    private static PropManager prop_manager;

    private static GameObject [] props;  

    // Start is called before the first frame update
    void Awake()
    {
        if (prop_manager == null)
        {
            prop_manager = this;
            DontDestroyOnLoad(prop_manager);
        }

        //props = GameObject.FindGameObjectsWithTag("Prop");

    }

    // Update is called once per frame
    private void Update()
    {
       if (test_doors)
        {
            right_door_animator.SetBool("Open", true);
            left_door_animator.SetBool("Open", true);
        } 
    }

    private bool CheckCount()
    {
        if (PROPS_CLEANED_COUNT == props.Length + 1)
            return true;

        return false;
    }

    public void IncreaseCount()
    {
        PROPS_CLEANED_COUNT += 1; 
    } 
}
