using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dump : MonoBehaviour
{
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

    internal void MoveTo(Vector3 dest)
    {
        this.destination = dest;
        this.moving = true;
    }

    internal void ResetPosition()
    {
        transform.position = new Vector3(-10f, 0f, 0f);
        this.moving = false;
    }
}
