using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPopup : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    public Slider masterSlider, soundSlider, musicSlider;
    public Toggle soundToggle, musicToggle;


    public void ToggleMaster(bool toggle)
    {
        masterSlider.enabled = toggle;

        soundSlider.enabled = toggle;
        soundToggle.enabled = toggle;

        musicSlider.enabled = toggle;
        musicToggle.enabled = toggle;

        if (toggle)
        {
            SetMasterVolume(masterSlider.value);
        }
        else
        {
            SetMasterVolume(masterSlider.minValue);
        }
    }

    public void ToggleSound (bool toggle)
    {
        soundSlider.enabled = toggle;
        if (toggle)
        {
            SetSoundVolume(soundSlider.value);
        }
        else
        {
            SetSoundVolume(soundSlider.minValue);
        }
    }

    public void ToggleMusic(bool toggle)
    {
        musicSlider.enabled = toggle;
        if (toggle)
        {
            SetMusicVolume(musicSlider.value);
        }
        else
        {
            SetMusicVolume(musicSlider.minValue);
        }
    }

    public void SetMasterVolume(float volume)
    {
        mainAudioMixer.SetFloat("masterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        mainAudioMixer.SetFloat("soundVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("soundVolume", volume);
    }
}
