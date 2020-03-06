using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A script which loads audio into memory (along with the relevant volume, sound type, and whether or not it should loop).
/// </summary>
public class SoundManager : MonoBehaviour
{
    Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();
    public AudioMixer mixer;
    public Sound[] sounds;
    private void Start()
    {
        if (IntegratedSoundManager.soundManager == null)
        {
            IntegratedSoundManager.soundManager = this;
            DontDestroyOnLoad(gameObject);
            foreach (Sound s in sounds)
            {
                if (!soundDict.ContainsKey(s.name.ToString()))
                {
                    soundDict.Add(s.name.ToString(), s);
                }
                else
                {
                    Debug.LogFormat("\"{0}\" already added to dictionary.",s.name.ToString());
                }
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound sound = soundDict[name];
            StartCoroutine(Play(sound));
        }
        else
        {
            Debug.LogFormat("soundDict doesn't contain \"{0}\".", name);
        }
    }
    public IEnumerator Play(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            yield return Play(soundDict[name]);
        }
    }
    public IEnumerator Play(Sound sound)
    {
        GameObject go = new GameObject
        {
            name = sound.name.ToString()
        };
        AudioSource audiosource = go.AddComponent<AudioSource>();
        //UpdateableAudioSource audiosource = go.AddComponent<UpdateableAudioSource>();
        //audiosource.Initialise();
        audiosource.clip = sound.clip;
        audiosource.loop = sound.loop;
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups(sound.type.ToString()).First();
        //audiosource.mixerGroup = mixer.FindMatchingGroups(sound.type.ToString()).First();
        audiosource.volume = sound.volume;
        //audiosource.volume = sound.volume * IntegratedSoundManager.GetVolume(sound.type) * IntegratedSoundManager.MasterVolume;
        //audiosource.originalVolume = sound.volume;
        //audiosource.soundType = sound.type;
        //audiosource.ID = IntegratedSoundManager.GetId(sound.type);
        //IntegratedSoundManager.AddAudioSource(sound.type, audiosource);
        audiosource.Play();
        yield return new WaitForSeconds(audiosource.clip.length);
        if (!sound.loop)
        {
            if (go != null)
            {
                audiosource.Stop();
                Destroy(go);
            }
        }
    }
}
public static class IntegratedSoundManager
{
    public static SoundManager soundManager;
	public static void PlaySoundAsync(SOUNDNAME soundName)
	{
		PlaySoundAsync(soundName.ToString());
	}
    public static void PlaySoundAsync(string name)
    {
        if (soundManager != null)
        {
            soundManager.PlaySound(name);
        }
        else
        {
            Debug.Log("No soundmanager has been implemented.");
        }
    }
	public static IEnumerator PlaySound(SOUNDNAME soundName)
	{
		yield return PlaySound(soundName.ToString());
	}
    public static IEnumerator PlaySound(string name)
    {
        if (soundManager != null)
        {
            yield return soundManager.Play(name);
        }
        else
        {
            Debug.Log("No soundmanager has been implemented.");
            yield return 0;
        }
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