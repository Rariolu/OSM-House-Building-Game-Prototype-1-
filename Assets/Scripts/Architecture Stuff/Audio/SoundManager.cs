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