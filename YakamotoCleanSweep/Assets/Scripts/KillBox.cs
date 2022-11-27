using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] Transform tp;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponentInParent<playerhealth>().TakeDamage(damage);
            //Debug.Log("You took one damage.");
            
            respawn(collision.collider.gameObject);
        }
    }
    
    private void respawn(GameObject go)
    {
        if (tp != null)
        {          
            go.transform.parent.position = tp.position;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene and then reset position
        }
    }
}
