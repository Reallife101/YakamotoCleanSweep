using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public static event Action OnAllClean;

    private int propsCleanedCount;

    private static PropManager prop_manager;

    [SerializeField]
    private static GameObject[] props;

    private void Start()
    {
        propsCleanedCount = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (prop_manager == null)
        {
            prop_manager = this;
            //DontDestroyOnLoad(prop_manager);
        }

        props = GameObject.FindGameObjectsWithTag("Prop");
    }

    public bool CheckCount()
    {
        return propsCleanedCount >= props.Length;
    }

    // Use an event for this instead later
    public void IncreaseCount()
    {
        propsCleanedCount += 1;

        if (CheckCount())
        {
            OnAllClean?.Invoke();
        }
    } 

    public int RemainingProps()
    {
        return props.Length - propsCleanedCount;
    }
}
