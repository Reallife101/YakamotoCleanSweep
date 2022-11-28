using UnityEngine;

public class MissileAudio : AudioController<Missile>
{
    private void OnEnable()
    {
        host.OnExplode += PlayExplosionSound;
    }

    private void PlayExplosionSound()
    {
        source.PlayOneShot(clips[0]);
    }

    private void OnDisable()
    {
        host.OnExplode -= PlayExplosionSound;
    }
}