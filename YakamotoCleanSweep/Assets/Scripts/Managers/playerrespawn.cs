using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerrespawn : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject SpawnPoint;
    // Update is called once per frame
    void Start(){
        SpawnPoint = GameObject.Find("SpawnPoint");
    }
    void Update()
    {
         if (this.transform.position[1] < -20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           this.transform.position = SpawnPoint.transform.position;
        }
    }
}
