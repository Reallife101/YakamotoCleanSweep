using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private FOV fov;

    [SerializeField]
    private GameObject missile;

    [SerializeField]
    private GameObject tipOfCannon;

    [SerializeField]
    private float fireRate;

    private float initialFiraRate;

    [SerializeField]
    private float reloadTime;

    private float initialReloadTime;

    [SerializeField]
    private float clipSize;

    private float initialClipSize;

    // Start is called before the first frame update
    void Start()
    {
        initialFiraRate = fireRate;
        initialReloadTime = reloadTime;
        initialClipSize = clipSize;
        fov = GetComponent<FOV>();
        fov.OnView += LookAtPlayer; 
    }

    private void LookAtPlayer(Transform target)
    {
        gameObject.transform.LookAt(target);
        fireRate -= Time.deltaTime;
        if (fireRate <= 0)
        {
            Shoot();
            fireRate = initialFiraRate;
            clipSize--;
        }
        if (clipSize == 0)
        {
            StartCoroutine("Reload");           
        }
    }

    private void Shoot()
    {
        Instantiate(missile, tipOfCannon.transform.position, tipOfCannon.transform.rotation.normalized);
    }
    

    private IEnumerator Reload()
    {
        WaitForSeconds wait = new WaitForSeconds(reloadTime);
        yield return wait;
        clipSize = initialClipSize;
    }
}
