using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ballot : MonoBehaviour
{
    public AudioClip[] paper_sound;
    public float volume = 0.5f;

    public Sprite[] spriteList;
    private int candidate = 0;
    private bool moving = false;
    private Vector3 destination;
    public float speed;
    private bool die_at_destination = false;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = spriteList[candidate];
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

    internal void SetCandidate(int v)
    {
        candidate = v;
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
