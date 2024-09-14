using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------------------------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXsource;

    [Header("--------------------------------------")]
    public AudioClip calmmusic;
    public AudioClip movement;
    public AudioClip rockhit;
    public AudioClip upgrade;

    private void Start()
    {
        musicSource.clip = calmmusic;
        musicSource.Play();
    }
}
