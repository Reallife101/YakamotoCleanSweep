using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    [SerializeField] private GameObject particlesys;
    [SerializeField] private float lifeTime = 4.0f;
    private GameObject a;   

    // Start is called before the first frame update
    void Start()
    {
       a =  Instantiate(particlesys,transform.position,transform.rotation);
         StartCoroutine(LifetimeRoutine());
    }
     private IEnumerator LifetimeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(a);
        Destroy(gameObject);
    }

  
}
