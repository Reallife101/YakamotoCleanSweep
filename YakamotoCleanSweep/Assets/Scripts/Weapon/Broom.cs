using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Broom : Weapon
{
    // Should melee weapons have a collider
    [SerializeField] private Collider col = null;



    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody player;
    [SerializeField] private Transform orientation;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float forwardSpeed;
    public float desiredYVel;


    private void Update()
    {    
        UpdateAttack();
    }

    protected override void Attack()
    {
        bool hitDetection = Physics.BoxCast(col.bounds.center, col.bounds.extents, eye.forward, 
                                            out hit, Quaternion.identity, range, enemyLayer);
        if (hitDetection)
        {
            print("Damage");
            DealDamage();
        }
    }

    private void FixedUpdate()
    {
        if (!playerMovement.isGrounded)
        {
            Glid();
        }

    }

    private void Glid()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 horvel = new Vector3(player.velocity.x, 0, player.velocity.z);
            Vector3 offsetVel = (orientation.forward * forwardSpeed) - horvel;
            player.AddForce(new Vector3(offsetVel.x, 0, offsetVel.z));

            if (player.velocity.y < desiredYVel)
            {
                Debug.Log("Lifting");
                player.AddForce(new Vector3(0, fallSpeed, 0));
            }

        }
    }
}