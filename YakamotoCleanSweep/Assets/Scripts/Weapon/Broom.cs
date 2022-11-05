using UnityEngine;

public class Broom : Weapon
{
    // Should melee weapons have a collider
    [SerializeField] private Collider col = null;

    private void Update()
    {
        UpdateAttack();
    }

    protected override void Attack()
    {
        bool hitDetection = Physics.BoxCast(col.bounds.center, col.bounds.extents, eye.forward, 
                                            out hit, Quaternion.identity, range, enemyLayer);
        if (hitDetection)
        {
            print("Damage");
            DealDamage();
        }
    }
}
