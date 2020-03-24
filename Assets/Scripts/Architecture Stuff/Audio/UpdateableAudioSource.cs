using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Linq;

/// <summary>
/// A monobehaviour script which plays audio and allows things like volume to be modified in real time (when main volume is changed).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class UpdateableAudioSource : MonoBehaviour
{
    AudioSource audiosource;
    public AudioSource AudioSource
    {
        get
        {
            return audiosource ?? (audiosource = GetComponent<AudioSource>());
        }
    }
    public IEnumerator Play(Sound sound, AudioMixer mixer)
    {
        AudioSource.clip = sound.clip;
        AudioSource.loop = sound.loop;
        AudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups(sound.type.ToString()).First();
        AudioSource.volume = sound.volume;
        AudioSource.Play();
        yield return new WaitForSeconds(AudioSource.clip.length);
        if (!sound.loop)
        {
            AudioSource.Stop();
            Destroy(gameObject);
        }
    }
    public void PlayAsync(Sound sound, AudioMixer mixer)
    {
        StartCoroutine(Play(sound, mixer));
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}