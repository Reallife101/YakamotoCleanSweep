using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExitManager : MonoBehaviour
{
    public PropManager pm;
    public GameObject exit;

    public bool forceExit;
    // Start is called before the first frame update
    void Start()
    {
        forceExit = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(pm.CheckCount() || forceExit)
        {
            exit.SetActive(true);
        }
    }
}
