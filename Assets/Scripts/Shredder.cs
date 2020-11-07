using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    public float shake_speed; 
    public float shake_amount;
    public float shake_duration;

    public AudioClip[] shred_sound;
    public float volume = 0.5f;

    private float shake_remaining;

    // Update is called once per frame
    void Update()
    {
        if (shake_remaining > 0)
        {
            GetComponent<Transform>().position = new Vector3(Mathf.Sin(Time.time * shake_speed) * shake_amount, Mathf.Cos(Time.time * shake_speed) * shake_amount, Mathf.Sin(Time.time * shake_speed) * shake_amount);
            shake_remaining -= Time.deltaTime;
        }
        else
        {
            GetComponent<Transform>().position = new Vector3(0f, 0f, 0f);
        }
    }

    internal void Shake()
    {
        shake_remaining = shake_duration;
        GetComponentInChildren<AudioSource>().PlayOneShot(shred_sound[UnityEngine.Random.Range(0, 10)], volume);
    }
}
