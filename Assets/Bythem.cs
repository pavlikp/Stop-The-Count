using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bythem : MonoBehaviour
{
    private bool moving = false;
    private Vector3 destination;
    public float speed;
    public SpriteRenderer evil_johny;

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
                StartCoroutine(FadeInEvilJohny());
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
        transform.position = new Vector3(10f, 0f, 0f);
        this.moving = false;
        evil_johny.color = new Color(255, 255, 255, 0f);
    }

    private IEnumerator FadeInEvilJohny()
    {
        int i = 0;
        int steps = 8;
        while (evil_johny.color != new Color(255, 255, 255, 1f))
        {
            yield return new WaitForSeconds(0.25f);
            evil_johny.color = new Color(255, 255, 255, 1f * i / steps);
            i++;
        }
    }
}
