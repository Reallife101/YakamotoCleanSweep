using UnityEngine;

public class RangedAudio : AudioController<Ranged>
{
    // First clip is attack, second is reload

    private void OnEnable()
    {
        host.OnAttack += AttackSound;
        host.OnReload += ReloadSound;
    }

    private void AttackSound()
    {
        source.PlayOneShot(clips[0]);
    }

    private void ReloadSound()
    {
        source.PlayOneShot(clips[1]);
    }

    private void OnDisable()
    {
        host.OnAttack -= AttackSound;
        host.OnReload -= ReloadSound;
    }
}
