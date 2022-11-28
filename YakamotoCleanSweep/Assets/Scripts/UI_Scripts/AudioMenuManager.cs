using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuManager : MonoBehaviour
{
    [SerializeField] private Slider masterVol;
    [SerializeField] private Slider effectsVol;
    [SerializeField] private Slider musicVol;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("MasterVolume", -1f) >= 0) {
            UIAudioController.SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
            masterVol.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.GetFloat("EffectsVolume", -1f) >= 0) {
            UIAudioController.SetEffectsVolume(PlayerPrefs.GetFloat("EffectsVolume"));
            effectsVol.value = PlayerPrefs.GetFloat("EffectsVolume");
        }
        if (PlayerPrefs.GetFloat("MusicVolume", -1f) >= 0) {
            UIAudioController.SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            musicVol.value = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        UIAudioController.SetMasterVolume(volume);
    }

    public static void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat("EffectsVolume", volume);
        UIAudioController.SetEffectsVolume(volume);
    }

    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        UIAudioController.SetMusicVolume(volume);
    }
}
