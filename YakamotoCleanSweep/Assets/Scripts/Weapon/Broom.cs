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
    [SerializeField] private float maxSpeed = 6;


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
            Vector3 direction = orientation.forward + orientation.right;
            player.MovePosition(player.position + direction.normalized * Time.deltaTime * maxSpeed);
            player.AddForce(transform.up * fallSpeed, ForceMode.Acceleration);
        }
    }
}