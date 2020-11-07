using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankTable : MonoBehaviour
{
    public AudioClip epic_win;
    public float volume = 0.5f;

    internal void PlayWinSound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(epic_win, volume);
    }
}
