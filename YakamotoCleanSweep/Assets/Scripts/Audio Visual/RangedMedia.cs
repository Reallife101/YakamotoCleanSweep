using UnityEngine;

public class RangedMedia : MediaController<Ranged>
{
    private static readonly int Reload = Animator.StringToHash("Reload");

    // Avoid using virtual functions for each OnEnable or Disable
    private void OnEnable()
    {
        host.OnAttack += AnimateAttack;
        host.OnReload += AnimateReload;
    }

    private void AnimateAttack()
    {
        anim.SetTrigger(WeaponMedia.Attack);
    }

    private void AnimateReload()
    {
        anim.SetTrigger(Reload);
    }

    private void OnDisable()
    {
        host.OnAttack -= AnimateAttack;
        host.OnReload -= AnimateReload;
    }
}
