using UnityEngine;
using System.Collections;

public abstract class Ranged : Weapon
{
    [SerializeField] protected float reloadTime = 1f;
    [SerializeField] protected int clipSize = 1;
    
    protected int bulletsRemaining = 1;

    private void Start()
    {
        bulletsRemaining = clipSize;
    }

    private void Update()
    {
        UpdateAttack();

        if (Input.GetButtonDown("Reload"))
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    protected void UpdateAmmo()
    {
        bulletsRemaining --;
        if (bulletsRemaining <= 0)
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    protected IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = clipSize;
    }

    protected void DrawRaycast(Vector3 dir)
    {
        Vector3 end = eye.position + dir * range;
        Debug.DrawLine(eye.position, end, Color.red, 10);
    }
}
