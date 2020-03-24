using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A script which loads audio into memory (along with the relevant volume, sound type, and whether or not it should loop).
/// </summary>
public class SoundManager : MonoBehaviour
{
    //Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();
    public AudioMixer mixer;
    public Sound[] sounds;
    private void Start()
    {
        foreach (Sound s in sounds)
        {
            IntegratedSoundManager.AddSound(s);
            //if (!soundDict.ContainsKey(s.name.ToString()))
            //{
            //    soundDict.Add(s.name.ToString(), s);
            //}
            //else
            //{
            //    Logger.Log("\"{0}\" already added to dictionary.",s.name.ToString());
            //}
        }
        IntegratedSoundManager.SetMixer(mixer);
        //if (IntegratedSoundManager.soundManager == null)
        //{
        //    IntegratedSoundManager.soundManager = this;
        //    DontDestroyOnLoad(gameObject);


        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    //public void PlaySound(string name)
    //{
    //    if (soundDict.ContainsKey(name))
    //    {
    //        Sound sound = soundDict[name];
    //        StartCoroutine(Play(sound));
    //    }
    //    else
    //    {
    //        Logger.Log("soundDict doesn't contain \"{0}\".", name);
    //    }
    //}
    //public IEnumerator Play(string name)
    //{
    //    if (soundDict.ContainsKey(name))
    //    {
    //        yield return Play(soundDict[name]);
    //    }
    //}
    //public IEnumerator Play(Sound sound)
    //{
    //    GameObject go = new GameObject
    //    {
    //        name = sound.name.ToString()
    //    };
    //    UpdateableAudioSource audiosource = go.AddComponent<UpdateableAudioSource>();
    //    yield return audiosource.Play(sound, mixer);
    //}
}
public static class IntegratedSoundManager
{
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
    static AudioMixer mixer;
    public static void SetMixer(AudioMixer _mixer)
    {
        mixer = _mixer;
    }
    //public static SoundManager soundManager;
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

        //if (soundManager != null)
        //{
        //    soundManager.PlaySound(name);
        //}
        //else
        //{
        //    Logger.Log("No soundmanager has been implemented.");
        //}
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
            GameObject go = new GameObject();
            go.name = name;
            UpdateableAudioSource audioSource = go.AddComponent<UpdateableAudioSource>();
            yield return audioSource.Play(sound, mixer);
        }
        else
        {
            Logger.Log("soundDict doesn't contain \"{0}\".", name);
        }
        //if (soundManager != null)
        //{
        //    yield return soundManager.Play(name);
        //}
        //else
        //{
        //    Logger.Log("No soundmanager has been implemented.");
        //    yield return 0;
        //}
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