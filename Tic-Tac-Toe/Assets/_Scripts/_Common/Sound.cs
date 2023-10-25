using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public AudioBase.SoundType soundType;

    public AudioClip clip;

    public AudioMixerGroup outputAudioMixerGroup;

    [Range(0f, 1f)]
    public float volume;

    [Range(1f, .1f)]
    public float pitchMin;
    [Range(1f, 3f)]
    public float pitchMax;

    [HideInInspector]
    public AudioSource source;
}
