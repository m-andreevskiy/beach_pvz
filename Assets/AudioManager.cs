using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio sources ---")]
    public AudioSource musicSource;
    public AudioSource brickLaunchSource;

    [Header("--- Sounds ---")]
    public AudioClip background;
    public AudioClip brickLaunch;
    

    void Start()
    {
        brickLaunchSource.clip = brickLaunch;

        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayBrickLaunchSound()
    {
        brickLaunchSource.Play();
    }
}
