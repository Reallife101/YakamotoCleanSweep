using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float range = 1f;
    [SerializeField] protected Transform attackPoint = null;

    private float attackTimer = 1f;
    protected RaycastHit hit;

    public void UpdateAttack()
    {
        attackTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && attackTimer <= 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        attackTimer = fireRate;
    }
}