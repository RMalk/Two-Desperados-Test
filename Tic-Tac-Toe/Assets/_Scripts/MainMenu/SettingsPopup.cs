using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsPopup : MonoBehaviour
{

    public AudioMixer mainAudioMixer;

    public void ToggleMaster(bool toggle)
    {

    }

    public void ToggleSound (bool toggle)
    {

    }

    public void ToggleMusic(bool toggle)
    {

    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("soundVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        mainAudioMixer.SetFloat("soundVolume", volume);
    }
}
