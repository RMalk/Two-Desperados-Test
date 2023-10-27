using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


//Note to self: boolean logic is a bitch without a pieace of papaer or at least some node based scripting. Avoid it in the future if possible
//TODO remove this comment as to not offend anyone reading this; tis was my atempt at righteous retribution against the code
public class SettingsPopup : MonoBehaviour
{
    public AudioMixer mainAudioMixer;

    public AudioManager audioManager;

    public Slider masterSlider, soundSlider, musicSlider;
    public Toggle soundToggle, musicToggle;

    public Color[] toggleColors;
    public Color[] sliderColors;

    //TODO PlayerPrefs; bummer

    public void ToggleMaster(bool toggle)
    {
        ToggleChange(toggle);
        for (int i = 0; i < 3; i++)
            SliderChange(toggle, i);

        SetMasterVolume(toggle ? masterSlider.value : masterSlider.minValue);
        if (toggle)
            ButtonPress();
    }

    void ToggleChange(bool toggle)
    {
        soundToggle.enabled = toggle;
        musicToggle.enabled = toggle;

        soundToggle.transform.GetChild(0).GetComponent<Image>().color = toggleColors[toggle ? 0 : 1];
        musicToggle.transform.GetChild(0).GetComponent<Image>().color = toggleColors[toggle ? 0 : 1];
    }

    public void ToggleSound (bool toggle)
    {
        soundSlider.enabled = toggle;
        SliderChange(toggle, 1);
        SetSoundVolume(toggle ? soundSlider.value : soundSlider.minValue);
        if (toggle)
            ButtonPress();
    }

    public void ToggleMusic(bool toggle)
    {
        musicSlider.enabled = toggle;
        SliderChange(toggle, 2);
        SetMusicVolume(toggle ? musicSlider.value : musicSlider.minValue);
        ButtonPress();
    }

    public void ButtonPress()
    {
        audioManager.PlaySounds(Utilities.SoundType.Click);
    }

    void SliderChange(bool toggle, int slider)
    {
        int index;

        switch (slider)
        {
            case 0:
                masterSlider.enabled = toggle;
                index = toggle ? 0 : 1;
                masterSlider.transform.GetChild(0).GetComponent<Image>().color = sliderColors[index];
                masterSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliderColors[index];
                break;
            case 1:
                soundSlider.enabled = toggle && soundToggle.isOn;
                index = (soundToggle.isOn && toggle) ? 0 : 1;
                soundSlider.transform.GetChild(0).GetComponent<Image>().color = sliderColors[index];
                soundSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliderColors[index];
                break;
            case 2:
                musicSlider.enabled = toggle && musicToggle.isOn;
                index = (musicToggle.isOn && toggle) ? 0 : 1;
                musicSlider.transform.GetChild(0).GetComponent<Image>().color = sliderColors[index];
                musicSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = sliderColors[index];
                break;
            default:
                Debug.Log("Panic, the world is a lie!");
                break;
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
