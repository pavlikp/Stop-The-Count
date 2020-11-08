using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public AudioClip ticking_sound;
    public float volume = 0.5f;

    public float shake_speed;
    public float shake_amount;

    private float shake_remaining = 0;

    internal bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            shaking = false;
        }
    }

    internal void StartCountdown()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(ticking_sound, volume);
    }

    internal void StartShaking(float time)
    {
        shake_remaining = time;
        shaking = true;
    }
}
