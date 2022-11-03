using UnityEngine;

public class SoapTalisman : Ranged
{
    protected override void Attack()
    {
        // For creating a puddle, either draw a separate raycast for the ground layer,
        // then create an object with the effect at the hit position

        bool hitDetection = Physics.Raycast(eye.position, eye.forward, out hit, range, enemyLayer);
        DrawRaycast(eye.forward);
        if (hitDetection)
        {
            DealDamage();
        }
        UpdateAmmo();
    }

    private void MakePuddle()
    {
        
    }
}
