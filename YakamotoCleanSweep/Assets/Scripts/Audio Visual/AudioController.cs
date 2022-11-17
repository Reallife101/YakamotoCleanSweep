using UnityEngine;
using UnityEngine.Audio;

public class AudioController<T> : MonoBehaviour
{
    protected static AudioMixer mixer = Resources.Load("mixer") as AudioMixer;

    [SerializeField] protected T host = default(T);
    [SerializeField] protected AudioSource source = null;
    [SerializeField] protected AudioClip[] clips = null;

    public static void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }
}
