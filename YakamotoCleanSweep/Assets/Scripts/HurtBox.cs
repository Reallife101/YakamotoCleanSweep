using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtBox : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.gameObject.GetComponent<health>().TakeDamage(damage);
            Debug.Log("You took one damage.");
        }
    }
}
