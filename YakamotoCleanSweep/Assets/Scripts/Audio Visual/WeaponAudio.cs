using UnityEngine;

public class WeaponAudio : AudioController<Weapon>
{
    private void OnEnable()
    {
        host.OnAttack += AttackSound;
    }

    private void AttackSound()
    {
        source.PlayOneShot(clips[0]);
    }

    private void OnDisable()
    {
        host.OnAttack -= AttackSound;
    }
}
