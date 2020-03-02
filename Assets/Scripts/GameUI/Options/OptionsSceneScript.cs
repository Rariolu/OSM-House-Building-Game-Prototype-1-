#pragma warning disable IDE0018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsSceneScript : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sldMasterVolume;
    public Slider sldSFXVolume;
    public Slider sldMusicVolume;

    void Awake()
    {
        SceneObjectScript menu;
        if (SceneObjectScript.InstanceExists(SCENE.Menu,out menu))
        {
            menu.SetActive(false);
        }
    }

    /// <summary>
    /// Attempts to retrieve the volume of the specified sound type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="f"></param>
    /// <returns></returns>
    bool GetVolume(SoundType type, out float f)
    {
        if (mixer == null)
        {
            Debug.Log("Mixer is null.");
            f = 0;
            return false;
        }
        return mixer.GetFloat(type.ToString(), out f);
        //return mixer.GetFloat("Volume (of {0})".FormatText(type), out f);
    }

    private void Start()
    {
        float masterVolume;
        if (GetVolume(SoundType.Master, out masterVolume))
        {
            sldMasterVolume.value = masterVolume;
        }

        float sfxVolume;
        if (GetVolume(SoundType.SFX, out sfxVolume))
        {
            sldSFXVolume.value = sfxVolume;
        }

        float musicVolume;
        if (GetVolume(SoundType.Music,out musicVolume))
        {
            sldMusicVolume.value = musicVolume;
        }
    }
}