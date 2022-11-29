using UnityEngine;

public class PlayerBarks : AudioController<playerhealth>
{
    /* --CLIPS (by inner list) --
    0 : Bronze
    1 : Silver
    2 : Gold
    3 : No Medal
    4 : Everything Cleaned
    5 : Object Cleaned
    6 : Damage Taken
    7 : Death
    8 : Fallen Off Map
    9 : Collectible Get
    */

    // USE THE PARENT CLIPS LIST FOR OVERFLOW
    [SerializeField] private SoundList[] maidBarks = null;
    [SerializeField] private SoundList[] butlerBarks = null;

    private SoundList[] barks = null; // possibly make these const or something idk mane

    private void PlayRandom(AudioClip[] sounds)
    {
        source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }

    private void Awake()
    {
        barks = PlayerPrefs.GetString("character", "").Equals("maid") ? maidBarks : butlerBarks;
    }

    private void OnEnable()
    {
        host.OnDamage += PlayDamageSound;
        host.OnDeath += PlayDeathSound;
        Prop.OnAnyPropCleaned += PlayCleanSound;
        PropManager.OnAllClean += PlayAllCleanSound;
    }

    private void PlayDamageSound()
    {
        PlayRandom(barks[6].sounds);
    }

    private void PlayDeathSound()
    {
        PlayRandom(barks[7].sounds);
    }
    
    private void PlayCleanSound()
    {
        PlayRandom(barks[5].sounds);
    }

    private void PlayAllCleanSound()
    {
        PlayRandom(barks[4].sounds);
    }

    private void OnDisable()
    {
        host.OnDamage -= PlayDamageSound;
        host.OnDeath -= PlayDeathSound;
        Prop.OnAnyPropCleaned -= PlayCleanSound;
        PropManager.OnAllClean -= PlayAllCleanSound;
    }
}
