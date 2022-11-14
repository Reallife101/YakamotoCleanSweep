using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponParticle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject particlesys;   //particle prefab to instantiate
    [SerializeField] private Weapon host;           
    // control how long each particle effect last
    [SerializeField] private float offset = 0.2f; // determine how much the emitter is in front of the player
    private GameObject a;
    private Transform eye = null;


    private void Awake(){
        eye = Camera.main.transform; 
    }
    private void OnEnable(){
        host.OnAttack += CreateParticle;   //when weapon attacks , subscribe CreateParticle to OnAttack Event
    }

    private void OnDisable(){
        host.OnAttack -= CreateParticle;
    }
    // Update is called once per frame
    private void CreateParticle(){
         Vector3 relativePos = eye.forward;
         Vector3 osvector = offset* relativePos;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
       a = Instantiate(particlesys,eye.position + osvector, rotation);
      
    }
    
   
}
