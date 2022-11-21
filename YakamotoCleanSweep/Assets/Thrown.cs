using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    [SerializeField] private LayerMask groundLayer = new LayerMask();
    [SerializeField] private GameObject puddle = null;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Prop") {
            other.gameObject.GetComponent<health>().TakeDamage(damage);
        }
        else if (other.gameObject.layer == 6) {
            MakePuddle(this.gameObject.transform.position);
        }
        Destroy(this.gameObject);
        
    }
    private void MakePuddle(Vector3 position)
    {
        
        // Change rotation later to align with the surface (perpendicular to its normal)
        Instantiate(puddle, position, Quaternion.identity);
    }
}
