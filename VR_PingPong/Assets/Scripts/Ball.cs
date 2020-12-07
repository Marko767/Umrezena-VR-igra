using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    int playerScore;
    public bool playing = true;
    public AudioSource hit;

    [SerializeField] Text player1_score_text;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        hit = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Paddle"))
        {
            hit.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Out"))
        {
            playerScore++;
            //playing = false;
            updateScores();
        }
    }
    void updateScores()
    {
        player1_score_text.text = "Player 1 : " + playerScore;
    }
}
