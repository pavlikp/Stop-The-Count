using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public AudioClip screech;
    public float volume = 0.75f;

    internal void PlaySound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(screech, volume);
    }
}
