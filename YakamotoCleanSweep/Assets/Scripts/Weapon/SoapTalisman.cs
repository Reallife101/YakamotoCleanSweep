using UnityEngine;

public class SoapTalisman : Ranged
{
    [SerializeField] private LayerMask groundLayer = new LayerMask();
    [SerializeField] private GameObject puddle = null;

    protected override void Attack()
    {
        // Measure if more optimal to use two raycasts for each layer or one that includes both,
        // then figure out what the object hit is
        bool groundDetection = Physics.Raycast(eye.position, eye.forward, out hit, range, groundLayer);
        if (groundDetection)
        {
            MakePuddle(hit.point + Vector3.up * 0.01f);
        }

        bool hitDetection = Physics.Raycast(eye.position, eye.forward, out hit, range, enemyLayer);
        DrawRaycast(eye.forward);
        if (hitDetection)
        {
            DealDamage();
        }
        UpdateAmmo();
    }

    private void MakePuddle(Vector3 position)
    {
        // Change rotation later to align with the surface (perpendicular to its normal)
        Instantiate(puddle, position, Quaternion.identity);
    }
}
