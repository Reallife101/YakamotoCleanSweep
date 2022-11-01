using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExitManager : MonoBehaviour
{
    public PropManager pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(pm.CheckCount())
        {
            Debug.Log("Everything is Clean!");
        }
    }
}
