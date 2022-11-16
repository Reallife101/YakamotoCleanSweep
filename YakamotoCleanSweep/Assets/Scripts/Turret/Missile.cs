using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private LayerMask obstacle;
    [SerializeField]
    private LayerMask playerMask;

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
        if (collision.gameObject.layer == playerMask.value)
        {
            // collision.gameObject.GetComponent<health>()
            Debug.Log("Missile hitting Player");
        }
        else if (collision.gameObject.layer == obstacle.value)
        {
            Debug.Log("Destroying");
            Destroy(gameObject);
        }
    }
}
