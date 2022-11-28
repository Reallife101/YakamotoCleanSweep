using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 180)]
    public float fov;

    [SerializeField]
    private float checkDelay;
    [SerializeField]
    private float viewDelay;

    private float startViewDelay;


    public LayerMask playerMask;
    public LayerMask obstacle;

    public event Action<Transform> OnView;

    private bool canSeePlayer;


    private void Start()
    {
        startViewDelay = viewDelay;
    }

    // Start is called before the first frame update
    void Update()
    {
        StartCoroutine("FOVRoutine");
    }


    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(checkDelay);

        if (!canSeePlayer)
            yield return wait;
        FindVisibleTargets();

    }

    // Update is called once per frame
    private void FindVisibleTargets()
    {
        
        Collider[] targetsInViewRaidus = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        if (targetsInViewRaidus.Length != 0)
        {
            canSeePlayer = true;
            Transform target = targetsInViewRaidus[0].transform;
            Vector3 directionToTargert = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTargert) < fov / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTargert, distanceToTarget, obstacle))
                {
                    // Debug.Log("View Delay " + viewDelay);
                    if (viewDelay <= 0)
                        OnView?.Invoke(target);
                    else
                        viewDelay -= Time.deltaTime;
                }
                else
                {
                    canSeePlayer = false;
                    viewDelay = startViewDelay;
                }
            }
        }
        else
        {
            canSeePlayer = false;
            viewDelay = startViewDelay;
        }

    }
}
