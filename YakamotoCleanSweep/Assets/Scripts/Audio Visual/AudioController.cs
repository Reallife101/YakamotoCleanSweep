using UnityEngine;
using UnityEngine.Audio;

public class AudioController<T> : MonoBehaviour
{
    private static AudioMixer mixer;

    [SerializeField] protected T host = default(T);
    [SerializeField] protected AudioSource source = null;
    [SerializeField] protected AudioClip[] clips = null;

    public static void GetMixer()
    {
        mixer = Resources.Load("Sounds/AudioMixer") as AudioMixer;
    }

    private void Awake()
    {
        AudioController<T>.GetMixer();
    }

    public static void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }

    public static void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectsVolume", volume);
    }

    public static void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
    }
}
