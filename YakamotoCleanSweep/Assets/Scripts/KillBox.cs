using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponentInParent<playerhealth>().TakeDamage(damage);
            //Debug.Log("You took one damage.");
            
            respawn();
        }
    }
    
    private void respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload scene and then reset position
    }
}
