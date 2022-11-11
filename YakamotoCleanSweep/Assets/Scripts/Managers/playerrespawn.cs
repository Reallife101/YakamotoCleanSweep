using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerrespawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject lowest_point;
    // Update is called once per frame
    void Update()
    {
         if (this.transform.position.y < lowest_point.transform.position.y - 10)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.transform.position = lowest_point.transform.position;
        }
    }
}
