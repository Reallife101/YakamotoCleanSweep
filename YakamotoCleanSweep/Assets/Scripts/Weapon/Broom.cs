using UnityEngine;

public class Broom : Weapon
{
    // Should melee weapons have a collider

    private void Update()
    {
        UpdateAttack();
    }

    protected override void Attack()
    {
        bool hitDetection = Physics.BoxCast(attackPoint.position, transform.localScale, transform.forward, 
                                            out hit, transform.rotation, range);
        if (hitDetection)
        {
            print("Take damage");
        }
    }
}
