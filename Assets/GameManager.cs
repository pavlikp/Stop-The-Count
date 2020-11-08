using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Ballot ballot;
    public GameObject tweet;
    public GameObject BlueBar;
    public GameObject RedBar;
    public GameObject timer_sprite;
    public GameObject rank_table;
    public Shredder shredder;
    public int BALLOT_COUNT;
    public int TIME_TO_COUNT;
    public int johny_chance;
    public Eagle eagle;
    public Dump ronald;
    public Bythem johny;
    public GameObject title_screen;
    public Text restart_text;
    public Clock clock;

    float timeRemaining;

    Ballot current_ballot;

    Dictionary<int, int> votes = new Dictionary<int, int>();
    Dictionary<int, float> percentages = new Dictionary<int, float>();

    bool send_next = true;
    private bool playing = false;

    Dictionary<String, float> states_table = new Dictionary<String, float>()
        {
            { "Wyoming", 26.6f},
            { "West Virginia", 29.6f},
            { "North Dakota", 31.7f},
            { "Oklahoma", 32.3f},
            { "Alaska", 33f},
            { "Idaho", 33.1f},
            { "Arkansas", 34.6f},
            { "South Dakota", 35.6f},
            { "Kentucky", 36.1f},
            { "Alabama", 36.5f},
            { "Utah", 37.2f},
            { "Tennessee", 37.4f},
            { "Mississippi", 38.9f},
            { "Nebraska", 39.1f},
            { "Louisiana", 39.8f},
            { "Montana", 40.4f},
            { "Indiana", 40.9f},
            { "Kansas", 41.1f},
            { "Missouri", 41.3f},
            { "South Carolina", 43.4f},
            { "Iowa", 44.9f},
            { "Ohio", 45.2f},
            { "Texas", 46.3f},
            { "Florida", 47.8f},
            { "North Carolina", 48.6f},
            { "Georgia", 49.4f},
            { "Wisconsin", 49.4f},
            { "Arizona", 49.6f},
            { "Pennsylvania", 49.6f},
            { "Nevada", 49.8f},
            { "Michigan", 50.5f},
            { "Minnesota", 52.5f},
            { "New Hampshire", 52.6f},
            { "Maine", 53.5f},
            { "Virginia", 54.1f},
            { "New Mexico", 54.2f},
            { "Colorado", 55.2f},
            { "Illinois", 55.3f},
            { "Oregon", 56.6f},
            { "New York", 58.3f},
            { "New Jersey", 58.5f},
            { "Delaware", 58.8f},
            { "Washington", 58.9f},
            { "Connecticut", 59.3f},
            { "Rhode Island", 59.4f},
            { "Maryland", 63.1f},
            { "Hawaii", 63.7f},
            { "Vermont", 64.9f},
            { "California", 65.1f},
            { "Massachusetts", 65.3f},
            { "District of Columbia", 92.6f}
        };
    private bool can_restart = true;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = TIME_TO_COUNT;

        // Initial votes
        votes.Add(1, 20);   // Ronald
        votes.Add(2, 4);    // Johny
        UpdatePerentages();
}

    private void UpdatePerentages()
    {
        float sum = votes.Sum(v => v.Value);
        foreach (KeyValuePair<int, int> entry in votes)
        {
            percentages[entry.Key] = entry.Value / sum * 100;
        }

        BlueBar.transform.localScale = new Vector3(RedBar.transform.localScale[0] * percentages[2] / 100, RedBar.transform.localScale[1], RedBar.transform.localScale[2]);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0 && playing)
        {
            timeRemaining -= Time.deltaTime;
            timer_sprite.transform.Rotate(Vector3.back * (360 * Time.deltaTime / TIME_TO_COUNT));

            if (send_next)
            {
                Ballot created_ballot = Instantiate(ballot, new Vector3(0f + Random.Range(-1f, 1f), -10f + Random.Range(-1f, 1f), 0f + Random.Range(-1f, 1f)), Quaternion.Euler(0, 0, Random.Range(-5, 5)));
                int candidate = Random.Range(0, 100) < johny_chance ? 2 : 1;
                if (timeRemaining > TIME_TO_COUNT * 3 / 4)
                {
                    candidate = Random.Range(0, 100) < ((johny_chance / 3) * 2) ? 2 : 1;
                    created_ballot.GetComponent<Ballot>().SetCandidate(candidate, 1);
                }
                else if (timeRemaining > TIME_TO_COUNT / 2)
                {
                    created_ballot.GetComponent<Ballot>().SetCandidate(candidate, 2);
                }
                else if (timeRemaining > TIME_TO_COUNT / 4)
                {
                    created_ballot.GetComponent<Ballot>().SetCandidate(candidate, 3);
                }
                else
                {
                    created_ballot.GetComponent<Ballot>().SetCandidate(candidate, 4);
                }
                created_ballot.GetComponent<Ballot>().MoveTo(new Vector3(0f, -1f, 0f));
                current_ballot = created_ballot;
                send_next = false;
            }

            if (!send_next)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    votes[current_ballot.GetComponent<Ballot>().GetCandidate()]++;
                    UpdatePerentages();

                    if(current_ballot.GetComponent<Ballot>().GetCandidate() == 1)
                    {
                        eagle.PlaySound();
                    }

                    current_ballot.GetComponent<Ballot>().MoveToAndDestroy(new Vector3(-15f, 0f, 0f));
                    send_next = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    if (current_ballot.GetComponent<Ballot>().GetCandidate() == 2)
                    {
                        eagle.PlaySound();
                    }

                    current_ballot.GetComponent<Ballot>().MoveToAndDestroy(new Vector3(15f, 0f, 0f));
                    shredder.Shake();
                    send_next = true;
                }
            }

            if (timeRemaining < TIME_TO_COUNT / 4 && !clock.shaking)
            {
                clock.StartShaking(timeRemaining);
            }
        }
        else if (playing)
        {
            StartCoroutine(StopTheCount());
            playing = false;
        }
        else if (!playing && Input.GetKeyDown(KeyCode.Space)  && can_restart)
        {
            Destroy(title_screen);

            rank_table.GetComponent<RankTable>().ResetPositonAndStopSounds();

            johny.GetComponent<Bythem>().ResetPosition();
            ronald.GetComponent<Dump>().ResetPosition();

            timeRemaining = TIME_TO_COUNT;

            votes = new Dictionary<int, int>();
            percentages = new Dictionary<int, float>();

            // Initial votes
            votes.Add(1, 20);   // Ronald
            votes.Add(2, 4);    // Johny
            UpdatePerentages();
            
            playing = true;
            send_next = true;
            can_restart = false;
            restart_text.enabled = false;

            clock.StartCountdown();
        }
    }

    private IEnumerator StopTheCount()
    {
        tweet.GetComponentInChildren<SpriteRenderer>().enabled = true;
        tweet.GetComponentInChildren<Tweet>().PlaySound();

        Destroy(current_ballot.gameObject);

        yield return new WaitForSeconds(3);

        float final_vote_percent = percentages[2];

        KeyValuePair<string, float> bottom_entry;
        KeyValuePair<string, float> middle_entry;
        KeyValuePair<string, float> top_entry;

        // Get final rank table
        if (states_table.First().Value > final_vote_percent)
        {
            bottom_entry = new KeyValuePair<string, float>("You", final_vote_percent);
            middle_entry = new KeyValuePair<string, float>("Wyoming", states_table["Wyoming"]);
            top_entry = new KeyValuePair<string, float>("West Virginia", states_table["West Virginia"]);
        } else if(states_table.Last().Value < final_vote_percent)
        {
            bottom_entry = new KeyValuePair<string, float>("Massachusetts", states_table["Massachusetts"]);
            middle_entry = new KeyValuePair<string, float>("District of Columbia", states_table["District of Columbia"]);
            top_entry = new KeyValuePair<string, float>("You", final_vote_percent);
        } else
        {
            foreach (KeyValuePair<string, float> entry in states_table)
            {
                if (entry.Value < final_vote_percent)
                {
                    bottom_entry = entry;
                } else
                {
                    middle_entry = new KeyValuePair<string, float>("You", final_vote_percent);
                    top_entry = entry;
                    break;
                }
            }
        }

        tweet.GetComponentInChildren<SpriteRenderer>().enabled = false;
        rank_table.GetComponent<RankTable>().MoveToCenter(top_entry, middle_entry, bottom_entry);
        if (final_vote_percent <= 50f)
        {
            rank_table.GetComponent<RankTable>().PlayLoseSound();
            ronald.MoveTo(new Vector3(-1f, 0f, 0f));
        } else
        {
            rank_table.GetComponent<RankTable>().PlayWinSound();
            johny.MoveTo(new Vector3(1f, 0f, 0f));
        }

        yield return new WaitForSeconds(5);

        can_restart = true;
        restart_text.enabled = true;
    }
}
