using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweet : MonoBehaviour
{
    public AudioClip stop_sound;
    public float volume = 0.5f;

    internal void PlaySound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(stop_sound, volume);
    }
}
