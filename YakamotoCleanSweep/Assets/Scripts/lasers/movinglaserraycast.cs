using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class movinglaserraycast : laserraycast
{
    
    
    
    [SerializeField] private float movingspeed;
    [SerializeField] private GameObject parentwall;
    private Vector3 startpoint;
    private Vector3 endpoint;
    private Vector3 direction;
   
   void Start(){
       Vector3 center = parentwall.transform.position;
       float halflength = parentwall.transform.lossyScale.x/2;
       float angle = parentwall.transform.rotation.y;
       float zend = ((float)transform.position.z) - (halflength*((float)Math.Cos(Math.PI * angle / 180.0f)));
       float xend = ((float)transform.position.x) - (halflength*  ((float)Math.Sin(Math.PI * angle / 180.0f))   );
     float zstart = ((float)transform.position.z) + (halflength*  ((float)Math.Cos(Math.PI * angle / 180.0f))       );
       float xstart = ((float)transform.position.x) + (halflength*  ((float)Math.Sin(Math.PI * angle / 180.0f))        );
       startpoint = new Vector3(xstart,parentwall.transform.position.y,zstart);
       endpoint = new Vector3(xend,parentwall.transform.position.y,zend);
       direction = new Vector3(endpoint.x-startpoint.x,0,0)/(halflength*2f);//endpoint.x-startpoint.x, startpoint.y,endpoint.z-startpoint.z);



   }
    //hit.collider.gameObject.GetComponent<health>().TakeDamage(damage);
    void Update(){
        //transform.localposition;
       
        //if(transform.position.x < endpoint.x && transform.position.z < endpoint.z &&transform.position.x >startpoint.x && transform.position.z >startpoint.z){
        transform.position = transform.position + movingspeed*direction;
       //}
        //else{
            //direction = -1*direction;
        //}
        laserline.SetPosition(0, transform.position);
        //laserline.SetPosition(1,transform.position+ (transform.forward*Range));
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit,playerlayer)){
           
            laserline.SetPosition(1,hit.point);
        }

        RaycastHit hit1;
        //Debug.Log("here1");
        if(Physics.Raycast(transform.position,transform.forward,out hit1) ){
             if(( (1 << hit1.collider.gameObject.layer)  & playerlayer) != 0){
           
            //laserline.SetPosition(1,hit1.point);
            //Debug.Log("here2");
            //Debug.Log(hit1.collider.gameObject.name);
            hit1.collider.gameObject.GetComponent<playerhealth>().TakeDamage(1);
            //Debug.Log("here3");
             }
        }
        Debug.Log("direction");
         Debug.Log(direction.x);
         Debug.Log(direction.z);
         Debug.Log("position");
        Debug.Log(endpoint);
    }
}