using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Ranged
{
    [SerializeField] private float throwForce = 50f;
    [SerializeField] private GameObject toThrow;
    // Start is called before the first frame update

    protected override void Attack() {
        GameObject projectile = Instantiate(toThrow, eye.position + eye.transform.forward * .3f, eye.rotation);

        Vector3 force = eye.transform.forward * throwForce;

        projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
