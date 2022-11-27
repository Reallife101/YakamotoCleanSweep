using UnityEngine;
using System.Collections;

public class Puddle : MonoBehaviour
{
    [SerializeField] private Collider col = null;
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private float slideForce = 1;
    [SerializeField] private float lifeTime = 1; // how many seconds the puddle lasts for

    private Vector3 extents = Vector3.one;

    private void Start()
    {
        StartCoroutine(LifetimeRoutine());

        // bounds are empty if the collider is not active
        extents = col.bounds.extents;
        Destroy(col);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position - Vector3.Scale(extents, transform.up), extents, transform.up, out hit, transform.rotation, extents.y, playerLayer))
        {
            Vector3 dir = hit.rigidbody.velocity.normalized;
            hit.rigidbody.AddForce(new Vector3 (dir.x, 0, dir.z) * slideForce, ForceMode.VelocityChange);
        }
    }

    // Automatically makes the puddle disappear after the given time limit
    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 origin = transform.position - Vector3.Scale(extents, transform.up);
        Gizmos.DrawWireCube(origin, extents * 2);
        Gizmos.DrawRay(origin, transform.up * extents.y);
        Gizmos.DrawWireCube(origin + transform.up * extents.y, extents * 2);
    }
}
