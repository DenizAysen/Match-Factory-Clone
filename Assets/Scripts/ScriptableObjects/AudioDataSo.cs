using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SFX/AudioData", fileName = "New Audio Data")]
public class AudioDataSo : ScriptableObject
{
    public AudioClip clip;
    [Range(0f,1f)] public float volume = 1;
    [Range(-3f, 3f)] public float pitch = 1;
}
