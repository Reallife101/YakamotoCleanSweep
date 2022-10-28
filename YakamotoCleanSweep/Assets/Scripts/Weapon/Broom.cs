using UnityEngine;

public class Broom : Weapon
{
    // Should melee weapons have a collider
    [SerializeField] private Vector3 hitSize = Vector3.one;

    private void Update()
    {
        UpdateAttack();
    }

    protected override void Attack()
    {
        bool hitDetection = Physics.BoxCast(eye.position, hitSize.normalized * 0.5f, eye.forward, 
                                            out hit, Quaternion.identity, range, enemyLayer);
        if (hitDetection)
        {
            print("Take damage");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + Camera.main.transform.forward * range);
        Gizmos.DrawWireCube(Camera.main.transform.position, hitSize.normalized);
    }
}
