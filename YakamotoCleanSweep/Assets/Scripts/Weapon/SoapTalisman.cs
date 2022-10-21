using System.Collections;
using UnityEngine;

public class SoapTalisman : Weapon
{
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private int clipSize = 1;

    private int bulletsRemaining = 1;

    private void Start()
    {
        bulletsRemaining = clipSize;
    }

    private void Update()
    {
        UpdateAttack();
    }

    protected override void Attack()
    {
        bool hitDetection = Physics.Raycast(attackPoint.position, transform.forward, out hit, range);
        if (hitDetection)
        {
            print("Take Damage");
        }
        Reload();
    }

    private void Reload()
    {
        bulletsRemaining --;
        if (bulletsRemaining <= 0)
        {
            StartCoroutine(ReloadRoutine());
        }
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = clipSize;
    }
}
