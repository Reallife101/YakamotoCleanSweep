using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float range = 1f;
    [SerializeField] protected LayerMask enemyLayer = new LayerMask();

    private float attackTimer = 1f;
    protected RaycastHit hit;
    protected Transform eye = null; // Camera transform for simplicity;

    private void Awake()
    {
        eye = Camera.main.transform;
    }

    public void UpdateAttack()
    {
        attackTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && attackTimer <= 0)
        {
            Attack();
            attackTimer = fireRate;
        }
    }

    protected abstract void Attack();
}