using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Weapon : MonoBehaviour
{
    public event Action OnAttack;

    [SerializeField] protected pauseLevel pause;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float range = 1f;
    [SerializeField] protected LayerMask enemyLayer = new LayerMask();
    [SerializeField] private List<AudioClip> attacks;

    private AudioSource attackSource;
    private float attackTimer = 1f;
    protected RaycastHit hit;
    protected Transform eye = null; // Camera transform for simplicity;

    private void Awake()
    {
        eye = Camera.main.transform;
        attackSource = GetComponent<AudioSource>();
    }

    public void UpdateAttack()
    {
        attackTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && attackTimer <= 0 && !pause.isPaused())
        {
            OnAttack?.Invoke();
            Attack();
            attackTimer = fireRate;
        }
    }

    protected void playAttackSound()
    {
        if (attackSource && attacks.Count > 0)
        {
            attackSource.PlayOneShot(attacks[UnityEngine.Random.Range(0, attacks.Count)], 1f);
        }
    }

    protected abstract void Attack();

    protected void DealDamage()
    {
        hit.collider.gameObject.GetComponent<health>().TakeDamage(damage);
    }
}