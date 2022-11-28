using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Missile : MonoBehaviour
{
    public event Action OnExplode;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private int damage; 


    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 forward = transform.forward;
        rb.AddForce(forward * speed, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerhealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
        }

        OnExplode?.Invoke();
    }
}
