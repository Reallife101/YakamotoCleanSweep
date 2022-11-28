using UnityEngine;
using System.Collections;

public class TurretAudio : AudioController<Turret>
{
    /* --CLIPS--
    0 : Hum
    1 : Lock On
    2 : Shoot */

    private FOV fov = null;

    // HAVE PLAY ON AWAKE ENABLED, CLIP FIELD AS HUM
    private void Awake()
    {
        fov = host.gameObject.GetComponent<FOV>();
    }

    private void OnEnable()
    {
        host.OnShoot += PlayShootSound;
        fov.OnView += PlayLockOnSound;
    }

    private void PlaySound(AudioClip sound)
    {
        source.Stop();
        source.clip = sound;
        source.Play();
        StartCoroutine(ResumeIdle());
    }

    private IEnumerator ResumeIdle()
    {
        yield return new WaitWhile(() => source.isPlaying);
        source.clip = clips[0];
        source.Play();
    }

    private void PlayShootSound()
    {
        PlaySound(clips[2]);
    }

    private void PlayLockOnSound(Transform _)
    {
        PlaySound(clips[1]);
    }   

    private void OnDisable()
    {
        host.OnShoot -= PlayShootSound;
        fov.OnView -= PlayLockOnSound;
    }
}