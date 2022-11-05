using UnityEngine;

public class Aerosol : Ranged
{
    [SerializeField] private float sprayAngle = 1f;
    [SerializeField] private int pelletAmount = 1; 
    [SerializeField] private float knockbackPower = 1f;

    [SerializeField] private Rigidbody playerBody;

    protected override void Attack()
    {
        Transform eye = Camera.main.transform;
        Vector3 currentDir = Vector3.RotateTowards(eye.forward, -eye.right, Mathf.Deg2Rad * sprayAngle * 0.5f, 0);
        float rotateDelta = sprayAngle / pelletAmount;

        for (int i = 0; i < pelletAmount; i++)
        {
            bool hitDetection = Physics.Raycast(eye.position, currentDir, out hit, range, enemyLayer); 
            DrawRaycast(currentDir);
            currentDir = Vector3.RotateTowards(currentDir, eye.right, Mathf.Deg2Rad * rotateDelta, 0);
            
            if (hitDetection)
            {
                DealDamage();
            }
        }

        UpdateAmmo();

        Knockback();
    }

    private void Knockback()
    {
        playerBody.AddForce(-eye.forward * knockbackPower, ForceMode.VelocityChange);
    }
}
