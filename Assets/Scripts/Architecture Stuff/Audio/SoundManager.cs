using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A script which loads audio into memory (along with the relevant volume,
/// sound type, and whether or not it should loop).
/// </summary>
public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Sound[] sounds;

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            IntegratedSoundManager.AddSound(s);
        }
        IntegratedSoundManager.SetMixer(mixer);
    }
}

public static class IntegratedSoundManager
{
    static AudioMixer mixer;
    static Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();

    public static void AddSound(Sound s)
    {
        if (!soundDict.ContainsKey(s.name.ToString()))
        {
            soundDict.Add(s.name.ToString(), s);
        }
        else
        {
            Logger.Log("\"{0}\" already added to dictionary.", s.name.ToString());
        }
    }

    static UpdateableAudioSource CreateAudioSource(string name = "audio")
    {
        GameObject go = new GameObject();
        go.name = name;
        UpdateableAudioSource audioSource = go.AddComponent<UpdateableAudioSource>();
        return audioSource;
    }

    public static IEnumerator PlaySound(SOUNDNAME soundName)
    {
        yield return PlaySound(soundName.ToString());
    }

    public static IEnumerator PlaySound(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound sound = soundDict[name];
            UpdateableAudioSource audioSource = CreateAudioSource(name);
            yield return audioSource.Play(sound, mixer);
        }
        else
        {
            Logger.Log("soundDict doesn't contain \"{0}\".", name);
        }
    }

    public static void PlaySoundAsync(SOUNDNAME soundName)
    {
        PlaySoundAsync(soundName.ToString());
    }

    public static void PlaySoundAsync(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound sound = soundDict[name];
            UpdateableAudioSource audioSource = CreateAudioSource(name);
            audioSource.PlayAsync(sound, mixer);
        }
        else
        {
            Logger.Log("soundDict doesn't contain \"{0}\".", name);
        }
    }

    public static void SetMixer(AudioMixer _mixer)
    {
        mixer = _mixer;
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop = false;
    public float volume = 1;
    public SoundType type;
}