using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------------------------------------")]
    [SerializeField] AudioSource musicSource;
    public AudioSource calmMusicSource; 
    [SerializeField] AudioSource SFXsource;

    [Header("--------------------------------------")]
    public AudioClip calmmusic;
    public AudioClip movement;
    public AudioClip rockhit;
    public AudioClip upgrade;
    public AudioClip Insert;
    public AudioClip Rockplode;
    public AudioClip Laser;
    public AudioClip EnemyHit;
    public AudioClip Melee;
    public AudioClip Mini;
    public AudioClip Boss;
    public AudioClip Button;
    public AudioClip BaseHit;
    public AudioClip Lose;
    public AudioClip Win;
    



    private void Start()
    {
        
        calmMusicSource.clip = calmmusic;
        calmMusicSource.Play();

        // Set the movement music clip and play it
        musicSource.clip = movement;
        musicSource.Play();

    }
    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }
}
