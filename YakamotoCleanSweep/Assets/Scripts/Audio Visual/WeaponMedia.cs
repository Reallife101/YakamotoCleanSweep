using UnityEngine;

public class WeaponMedia : MediaController<Weapon>
{
    private void OnEnable()
    {
        host.OnAttack += AnimateAttack;
    }

    private void AnimateAttack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnDisable()
    {
        host.OnAttack -= AnimateAttack;
    }
}
