﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ballot : MonoBehaviour
{
    public Sprite[] spriteList;
    private int candidate = 0;
    private bool moving = false;
    private Vector3 destination;
    public float speed;

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
            }
        }
    }

    internal void SetCandidate(int v)
    {
        candidate = v;
    }

    internal void MoveTo(Vector3 dest)
    {
        this.destination = dest;
        this.moving = true;
    }
}
