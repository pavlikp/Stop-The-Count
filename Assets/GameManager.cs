using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject ballot;
    public GameObject tweet;
    public GameObject timer_sprite;
    public int BALLOT_COUNT;
    public int TIME_TO_COUNT;

    float timeRemaining;

    GameObject[] ballot_instance;
    int[] ballot_candidates;

    int current_ballot = 0;

    Dictionary<int, int> votes = new Dictionary<int, int>();
    Dictionary<int, float> percentages = new Dictionary<int, float>();

    bool send_next = true;
    


    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = TIME_TO_COUNT;

        // Initial votes
        votes.Add(1, 10);   // Donald
        votes.Add(2, 1);    // Joe
        UpdatePerentages();


        // Initialize ballot candidate votes
        ballot_candidates = new int[BALLOT_COUNT];
        for (int i = 0; i < BALLOT_COUNT; i++)
        {
            ballot_candidates[i] = Random.Range(1, 3);
        }

        // Initialize ballot game objects
        ballot_instance = new GameObject[BALLOT_COUNT];
        for (int i = 0; i < BALLOT_COUNT; i++)
        {
            ballot_instance[i] = Instantiate(ballot, new Vector3(0f + Random.Range(-1f,1f), -10f + Random.Range(-1f, 1f), 0f + Random.Range(-1f, 1f)), Quaternion.Euler(0, 0, Random.Range(-5, 5)));
            ballot_instance[i].GetComponent<Ballot>().SetCandidate(ballot_candidates[i]);
        }
}

    private void UpdatePerentages()
    {
        float sum = votes.Sum(v => v.Value);
        foreach (KeyValuePair<int, int> entry in votes)
        {
            percentages[entry.Key] = entry.Value / sum * 100;
        }
        print("bidet votes: " + percentages[2]);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timer_sprite.transform.Rotate(Vector3.back * (360 * Time.deltaTime / TIME_TO_COUNT));

            if (send_next)
            {
                ballot_instance[current_ballot].GetComponent<Ballot>().MoveTo(new Vector3(0f, 0f, 0f));
                send_next = false;
            }

            if (!send_next)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    votes[ballot_candidates[current_ballot]]++;
                    UpdatePerentages();

                    ballot_instance[current_ballot].GetComponent<Ballot>().MoveTo(new Vector3(-15f, 0f, 0f));
                    current_ballot++;
                    send_next = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ballot_instance[current_ballot].GetComponent<Ballot>().MoveTo(new Vector3(15f, 0f, 0f));
                    current_ballot++;
                    send_next = true;
                }
            }
        }
        else
        {
            StopTheCount();
        }
    }

    private void StopTheCount()
    {
        tweet.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
}
