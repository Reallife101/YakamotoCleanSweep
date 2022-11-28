using UnityEngine;

public class LaserAudio : AudioController<laserraycast>
{
    /* --CLIPS--
    0 : Hit
    1 : Buzz */

    // HAVE PLAY ON AWAKE ENABLED, CLIP FIELD AS BUZZ
    private void OnEnable()
    {
        host.OnHit += PlayHitSound;
    }

    private void PlayHitSound()
    {
        source.PlayOneShot(clips[0]);
    }

    private void OnDisable()
    {
        host.OnHit -= PlayHitSound;
    }
}