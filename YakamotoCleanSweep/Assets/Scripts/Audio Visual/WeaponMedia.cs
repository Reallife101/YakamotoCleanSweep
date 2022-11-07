using UnityEngine;

public class WeaponMedia : MediaController<Weapon>
{
    public static readonly int Attack = Animator.StringToHash("Attack");

    private void OnEnable()
    {
        host.OnAttack += AnimateAttack;
    }

    private void AnimateAttack()
    {
        anim.SetTrigger(Attack);
    }

    private void OnDisable()
    {
        host.OnAttack -= AnimateAttack;
    }
}
