using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExitManager : MonoBehaviour
{
    public PropManager pm;
    public GameObject exit;
    public GameObject door;

    public bool forceExit;
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(true);
        forceExit = false;
        exit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(pm.CheckCount() || forceExit)
        {
            door.SetActive(false);
            exit.SetActive(true);
        }
    }
}
