using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ballot;
    public int BALLOT_COUNT;

    GameObject[] ballot_instance;
    int[] ballot_candidates;

    int current_ballot = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize ballot candidates
        ballot_candidates = new int[BALLOT_COUNT];
        for (int i = 0; i < BALLOT_COUNT; i++)
        {
            ballot_candidates[i] = Random.Range(1, 3);
        }

        // Initialize ballot game objects
        ballot_instance = new GameObject[BALLOT_COUNT];
        for (int i = 0; i < BALLOT_COUNT; i++)
        {
            ballot_instance[i] = Instantiate(ballot, new Vector3(0f + Random.Range(-1f,1f), -10f + Random.Range(-1f, 1f), 0f + Random.Range(-1f, 1f)), Quaternion.identity);
            ballot_instance[i].GetComponent<Ballot>().SetCandidate(ballot_candidates[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ballot_instance[current_ballot].GetComponent<Ballot>().MoveTo(new Vector3(0f, 0f, 0f));
            current_ballot++;
        }
    }
}
