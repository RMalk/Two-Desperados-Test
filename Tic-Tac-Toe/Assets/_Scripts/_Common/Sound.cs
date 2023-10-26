using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public Utilities.SoundType soundType;

    public AudioClip clip;

    public AudioMixerGroup outputAudioMixerGroup;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(1f, .1f)]
    public float pitchMin = 1f;
    [Range(1f, 3f)]
    public float pitchMax = 1f;

    [HideInInspector]
    public AudioSource source;
}
