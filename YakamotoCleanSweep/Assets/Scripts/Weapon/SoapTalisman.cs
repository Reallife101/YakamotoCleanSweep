using UnityEngine;

public class SoapTalisman : Ranged
{
    [SerializeField] private LayerMask groundLayer = new LayerMask();
    [SerializeField] private float throwForce = 1f;
    [SerializeField] private GameObject puddle = null;
    [SerializeField] private GameObject projectile = null;

    protected override void Attack()
    {
        // Measure if more optimal to use two raycasts for each layer or one that includes both,
        // then figure out what the object hit is
        bool groundDetection = Physics.Raycast(eye.position, eye.forward, out hit, range, groundLayer);
        if (groundDetection)
        {
            MakePuddle(hit.point + hit.normal * 0.01f);
        }

        bool hitDetection = Physics.Raycast(eye.position, eye.forward, out hit, range, enemyLayer);
        DrawRaycast(eye.forward);
        if (hitDetection)
        {
            DealDamage();
        }
        UpdateAmmo();

        // Create projectile for visual feedback
        GameObject projectile = Instantiate(this.projectile, eye.position + eye.transform.forward * .3f, eye.rotation);
        Vector3 force = eye.transform.forward * throwForce;
        projectile.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }

    private void MakePuddle(Vector3 position)
    {
        // Change rotation later to align with the surface (perpendicular to its normal)
        Instantiate(puddle, position, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
    }
}
