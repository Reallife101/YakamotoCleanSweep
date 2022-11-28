using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class laserraycast : MonoBehaviour
{
    public event Action OnHit;
    
    protected LineRenderer laserline;
  
    [SerializeField] protected LayerMask playerlayer;
    void Awake()
    {
        laserline = GetComponent<LineRenderer>();
        laserline.SetPosition(0, transform.position);
        //laserline.SetPosition(1,transform.position+ (transform.forward*Range));
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,playerlayer)){
           
            laserline.SetPosition(1,hit.point);
        }

    }
    //hit.collider.gameObject.GetComponent<health>().TakeDamage(damage);
    void Update(){
        RaycastHit hit1;
        Debug.Log("here1");
        if(Physics.Raycast(transform.position,transform.forward,out hit1) ){
             if(( (1 << hit1.collider.gameObject.layer)  & playerlayer) != 0){
           
            //laserline.SetPosition(1,hit1.point);
            Debug.Log("here2");
            Debug.Log(hit1.collider.gameObject.name);
            hit1.collider.gameObject.GetComponent<playerhealth>().TakeDamage(1);
            Debug.Log("here3");

            OnHit?.Invoke();
             }
        }
    }
    
}
