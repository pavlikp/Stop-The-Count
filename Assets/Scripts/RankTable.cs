using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankTable : MonoBehaviour
{
    public AudioClip epic_win;
    public AudioClip epic_fail;
    public float volume = 0.5f;

    private bool moving = false;
    private Vector3 destination;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                moving = false;
            }
        }
    }

    internal void PlayWinSound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(epic_win, volume);
    }

    internal void PlayLoseSound()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(epic_fail, volume);
    }

    internal void MoveToCenter(KeyValuePair<string, float> top_entry, KeyValuePair<string, float> middle_entry, KeyValuePair<string, float> bottom_entry)
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            if (text.gameObject.name == "TopState")
            {
                text.text = top_entry.Key.ToUpper();
            }
            if (text.gameObject.name == "MiddleState")
            {
                text.text = middle_entry.Key.ToUpper();
            }
            if (text.gameObject.name == "BottomState")
            {
                text.text = bottom_entry.Key.ToUpper();
            }
            if (text.gameObject.name == "TopVote")
            {
                text.text = System.Math.Round(top_entry.Value,1).ToString();
            }
            if (text.gameObject.name == "MiddleVote")
            {
                text.text = System.Math.Round(middle_entry.Value, 1).ToString();
            }
            if (text.gameObject.name == "BottomVote")
            {
                text.text = System.Math.Round(bottom_entry.Value, 1).ToString();
            }
        }

        this.destination = new Vector3(0f,0f,0f);
        this.moving = true;
    }

    internal void ResetPositonAndStopSounds()
    {
        transform.position = new Vector3(0f, 8f, 0f);

        GetComponentInChildren<AudioSource>().Stop();
    }
}
