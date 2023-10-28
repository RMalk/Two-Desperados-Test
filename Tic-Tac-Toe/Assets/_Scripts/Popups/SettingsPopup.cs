using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Note to self: boolean logic is a bitch without a pieace of papaer or at least some node based scripting. Avoid it in the future if possible
//TODO remove this comment as to not offend anyone reading this; tis was my atempt of righteous retribution against the code
public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    [SerializeField] private AudioManager audioManager;

    [Header("Master Settings")]
    [SerializeField] private Toggle masterToggle;
    [SerializeField] private Slider masterSlider;


    [Header("Sound Settings")]
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Slider soundSlider;

    [Header("Music Settings")]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider musicSlider;

    [Header("Color Variation")]
    [SerializeField] private Color[] toggleColors;
    [SerializeField] private Color[] sliderColors;

    public void OnEnable ()
    {
        if (PlayerPrefs.HasKey("Volume Master"))
        {
            masterToggle.isOn = PlayerPrefs.GetInt("Volume Master Toggle") > 0 ? true : false;
            soundToggle.isOn = PlayerPrefs.GetInt("Volume Sound Toggle") > 0 ? true : false;
            musicToggle.isOn = PlayerPrefs.GetInt("Volume Music Toggle") > 0 ? true : false;

            masterSlider.value = PlayerPrefs.GetFloat("Volume Master");
            soundSlider.value = PlayerPrefs.GetFloat("Volume Sound");
            musicSlider.value = PlayerPrefs.GetFloat("Volume Music");
        }
    }

    public void ToggleMaster(bool toggle)
    {
        ToggleChange(toggle);
        for (int i = 0; i < 3; i++)
            SliderChange(toggle, i);

        SetMasterVolume(toggle ? masterSlider.value : masterSlider.minValue);
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
        if (gameObject.activeSelf)
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
        mainAudioMixer.SetFloat("masterVolume", Utilities.TweakVolume(volume * (masterToggle.isOn ? 1 : 0)));
    }

    public void SetSoundVolume(float volume)
    {
        mainAudioMixer.SetFloat("soundVolume", Utilities.TweakVolume(volume * (soundToggle.isOn ? 1 : 0)));
    }

    public void SetMusicVolume(float volume)
    {
        mainAudioMixer.SetFloat("musicVolume", Utilities.TweakVolume(volume * (musicToggle.isOn ? 1 : 0)));
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("Volume Master Toggle", masterToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Volume Sound Toggle", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Volume Music Toggle", musicToggle.isOn ? 1 : 0);

        PlayerPrefs.SetFloat("Volume Master", masterSlider.value);
        PlayerPrefs.SetFloat("Volume Sound", soundSlider.value);
        PlayerPrefs.SetFloat("Volume Music", musicSlider.value);
    }
}
