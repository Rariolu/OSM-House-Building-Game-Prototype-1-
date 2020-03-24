using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop = false;
    public float volume = 1;
    public SoundType type;
}