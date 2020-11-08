using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ballot : MonoBehaviour
{
    public AudioClip[] paper_sound;
    public float volume = 0.5f;

    public Sprite[] spriteListJohny;
    public Sprite[] spriteListRonald;
    private int candidate = 0;
    private int difficulty = 0;
    private bool moving = false;
    private Vector3 destination;
    public float speed;
    private bool die_at_destination = false;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        int ballot_max_range = difficulty == 3 ? 6 : difficulty == 2 ? 3 : 1;
        if (candidate == 1)
        {
            renderer.sprite = spriteListRonald[Random.Range(0, ballot_max_range)];
        }
        else
        {
            renderer.sprite = spriteListJohny[Random.Range(0, ballot_max_range)];
        }
        if (difficulty == 3)
        {
            if (Random.Range(0, 4) < 1)
            {
                renderer.flipX = enabled;
                renderer.flipY = enabled;
            }
        }
        if (difficulty == 4)
        {
            if (Random.Range(0, 4) < 1)
            {
                renderer.flipX = enabled;
            }
            else if (Random.Range(0, 4) < 1)
            {
                renderer.flipY = enabled;
            }
            else if (Random.Range(0, 4) < 1)
            {
                renderer.flipX = enabled;
                renderer.flipY = enabled;
            }
        }
    }

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
                if (die_at_destination)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    internal void SetCandidate(int candidate, int difficulty)
    {
        this.candidate = candidate;
        this.difficulty = difficulty;
    }

    internal int GetCandidate()
    {
        return candidate;
    }

    internal void MoveTo(Vector3 dest)
    {
        this.destination = dest;
        this.moving = true;
        GetComponentInChildren<AudioSource>().PlayOneShot(paper_sound[UnityEngine.Random.Range(0,10)], volume);
    }

    internal void MoveToAndDestroy(Vector3 dest)
    {
        MoveTo(dest);
        die_at_destination = true;
    }
}
