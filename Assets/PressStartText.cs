using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressStartText : MonoBehaviour
{
    public float flash_interval;
    public Color[] colors;

    private float time_remaining = 0;
    private int current_color = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
        }
        else
        {
            time_remaining = flash_interval;

            GetComponent<Text>().color = colors[current_color];
            current_color++;
            if (current_color >= colors.Length)
            {
                current_color = 0;
            }
        }
    }
}
