using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public static SFXManager Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Play(AudioDataSo audioDataSo)
    {
        audioSource.clip = audioDataSo.clip;
        audioSource.volume = audioDataSo.volume;
        audioSource.pitch = audioDataSo.pitch;
        audioSource.Play();
    }
}
