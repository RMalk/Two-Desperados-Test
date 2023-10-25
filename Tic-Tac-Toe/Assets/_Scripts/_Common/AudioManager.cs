using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake ()
    {
        CreateSoundGroups();

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject(sounds[i].clip.name);
            soundObject.transform.parent = transform.GetChild((int)sounds[i].soundType);
            sounds[i].source = soundObject.AddComponent<AudioSource>();

            sounds[i].source.playOnAwake = false;

            sounds[i].source.clip = sounds[i].clip;

            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.outputAudioMixerGroup = sounds[i].outputAudioMixerGroup;
        }
    }

    void CreateSoundGroups ()
    {
        int typeCount = Enum.GetNames(typeof(AudioBase.SoundType)).Length;
        int[] soundTypeCount = new int[typeCount];

        string[] names = System.Enum.GetNames(typeof(AudioBase.SoundType));

        for (int i = 0; i < sounds.Length; i++)
        {
            soundTypeCount[(int)sounds[i].soundType]++;
        }

        for (int i = 0; i < typeCount; i++)
        {
            if (soundTypeCount[i] > 0)
            {
                GameObject soundGroup = new GameObject(names[i] + (soundTypeCount[i] > 1 ? "s" : ""));
                soundGroup.transform.parent = transform;
            }
        }

    }

    public void PlaySounds(AudioBase.SoundType soundType)
    {
        int soundCount = transform.GetChild((int)soundType).childCount;
        int randomSound = 0;
        if (soundCount != 1)
        {
            randomSound = UnityEngine.Random.Range(0, soundCount - 1);
        }

        soundCount = 0;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == soundType)
            {
                if (soundCount == randomSound)
                {
                    float randomPitch = UnityEngine.Random.Range(sounds[i].pitchMin, sounds[i].pitchMax);
                    sounds[i].source.pitch = randomPitch;
                    sounds[i].source.Play();
                    break;
                }
                soundCount++;
            }
        }
    }
}
