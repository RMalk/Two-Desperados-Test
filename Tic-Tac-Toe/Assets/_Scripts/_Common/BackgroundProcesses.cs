using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class BackgroundProcesses : MonoBehaviour
{
    [SerializeField] private AudioMixer mainAudioMixer;

    void OnEnable ()
    {
        SceneManager.LoadScene("Background", LoadSceneMode.Additive);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    void Start()
    {
        //Note to self: audio CANNOT be modified during OnEnable; nightmare fuel right there
        if (PlayerPrefs.HasKey("Volume Master"))
        {
            mainAudioMixer.SetFloat("masterVolume",
                Utilities.TweakVolume(PlayerPrefs.GetFloat("Volume Master") * PlayerPrefs.GetInt("Volume Master Toggle"))
            );
            mainAudioMixer.SetFloat("soundVolume",
                Utilities.TweakVolume(PlayerPrefs.GetFloat("Volume Sound") * PlayerPrefs.GetInt("Volume Sound Toggle"))
            );
            mainAudioMixer.SetFloat("musicVolume",
                Utilities.TweakVolume(PlayerPrefs.GetFloat("Volume Music") * PlayerPrefs.GetInt("Volume Music Toggle"))
            );
        }
    }
}
