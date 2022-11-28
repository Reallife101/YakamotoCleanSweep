using UnityEngine;
using System.Collections;
using System;

public class Puddle : MonoBehaviour
{
    public static event Action OnPuddleEnter;

    [SerializeField] private Collider col = null;
    [SerializeField] private LayerMask playerLayer = new LayerMask();
    [SerializeField] private float slideForce = 1;
    [SerializeField] private float lifeTime = 1; // how many seconds the puddle lasts for

    private void Start()
    {
        StartCoroutine(LifetimeRoutine());
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.BoxCast(col.bounds.center, col.bounds.extents, transform.up, out hit, Quaternion.identity, transform.localScale.y, playerLayer))
        {
            OnPuddleEnter?.Invoke();
            hit.rigidbody.AddForce(new Vector3(hit.rigidbody.velocity.x, 0, hit.rigidbody.velocity.z) * slideForce, ForceMode.Force);
        }
    }

    // Automatically makes the puddle disappear after the given time limit
    private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}