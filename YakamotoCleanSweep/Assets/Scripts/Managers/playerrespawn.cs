using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerrespawn : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private int lowest_point = -10;
    [SerializeField] private playerhealth ph_host;
    //[SerializeField] private GameObject playerBody;
    // Update is called once per frame
     private void OnEnable(){
        ph_host.OnDeath += respawn;   //subscribe to on Death event
    }

    /*
     void Update()
    {
         if (playerBody.transform.position.y < lowest_point) //we made an axiom so that if the player is more than 10
        {                                              // units under the lowest point of the lowest game object, player dies
            respawn();
        }
    }
    */

    private void respawn()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene and then reset position
    }
}
