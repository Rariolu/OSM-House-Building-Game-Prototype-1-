using UnityEngine;
using System.Collections;

/// <summary>
/// A monobehaviour script which plays audio and allows things like volume to be modified in real time (when main volume is changed).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class UpdateableAudioSource : MonoBehaviour
{
    static UpdateableAudioSource _musicInstance;
    public static UpdateableAudioSource MusicInstance
    {
        get
        {
            return _musicInstance;
        }
    }
    AudioSource audiosource;
    public AudioClip clip
    {
        get
        {
            return audiosource.clip;
        }
        set
        {
            audiosource.clip = value;
        }
    }
    public bool loop
    {
        get
        {
            return audiosource.loop;
        }
        set
        {
            audiosource.loop = value;
        }
    }
    public float originalVolume = 1;
    public float volume
    {
        get
        {
            return audiosource.volume;
        }
        set
        {
            audiosource.volume = value;
        }
    }
    SoundType soundtype = SoundType.SFX;
    public SoundType soundType
    {
        get
        {
            return soundtype;
        }
        set
        {
            soundtype = value;
            if (soundtype == SoundType.Music)
            {
                _musicInstance = this;
            }
        }
    }
    public int ID = 0;
    public void Initialise()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void ResetAudioSourceVolume()
    {
        volume = originalVolume * IntegratedSoundManager.GetVolume(soundType) * IntegratedSoundManager.MasterVolume;
    }
    public void Play()
    {
        audiosource.Play();
    }
    public void Stop()
    {
        if (audiosource != null)
        {
            audiosource.Stop();
        }
    }
    private void OnDestroy()
    {
        IntegratedSoundManager.DeleteAudioSource(soundType, ID);
    }
}