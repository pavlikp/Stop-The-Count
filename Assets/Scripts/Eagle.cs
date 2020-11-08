using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public AudioClip screech;
    public float volume = 0.75f;

    public float shake_speed;
    public float shake_amount;
    public float shake_duration;

    public Sprite normal;
    public Sprite screeching;

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
            GetComponentInChildren<SpriteRenderer>().sprite = normal;
        }
    }

    internal void PlaySound()
    {
        shake_remaining = shake_duration;
        GetComponentInChildren<AudioSource>().PlayOneShot(screech, volume);
        GetComponentInChildren<SpriteRenderer>().sprite = screeching;
    }
}
