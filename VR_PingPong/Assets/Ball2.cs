using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball2 : MonoBehaviour
{
    bool playing = true;

    int player1_gameScore;
    int player2_gameScore;

    int player1_setScore;
    int player2_setScore;

    int lastPlayerHit;

    int lastTableHit;

    bool gameInProgress;
    public bool matchInProgress;

    int isServing;

    //servis
    bool firstHit;
    bool secondHit;

    public AudioSource hitSound;

    [SerializeField] Text player1_score_text;
    [SerializeField] Text player2_score_text;

    // Start is called before the first frame update
    void Start()
    {
        player1_gameScore = 0;
        player2_gameScore = 0;
        player1_setScore = 0;
        player2_setScore = 0;

        lastPlayerHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;
        matchInProgress = true;
        isServing = 1;

        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("BluePaddle") || collision.transform.CompareTag("OrangePaddle") || collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Player1ServeArea") || collision.transform.CompareTag("Player1TableArea") || collision.transform.CompareTag("Player2ServeArea") || collision.transform.CompareTag("Player2TableArea") || collision.transform.CompareTag("Walls"))
        {
            hitSound.Play();
        }

        if (collision.transform.CompareTag("BluePaddle"))
        {
            updateLastPlayerHit(1);
            playing = true;
        }
        else if (collision.transform.CompareTag("OrangePaddle"))
        {
            updateLastPlayerHit(2);
            playing = true;
        }

        if (collision.transform.CompareTag("Player1Table")){
            lastTableHit = 1;
        }
        else if (collision.transform.CompareTag("Player2Table")){
            lastTableHit = 2;
        }

            if (lastPlayerHit != 0)
        {
            if (matchInProgress && playing)
            {
                if (collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Player1ServeArea") || collision.transform.CompareTag("Player1TableArea") || collision.transform.CompareTag("Player2ServeArea") || collision.transform.CompareTag("Player2TableArea") || collision.transform.CompareTag("Walls"))
                {
                    if (!firstHit && lastTableHit == 1)
                    {
                        firstHit = true;
                    }
                    else if (firstHit && lastTableHit == 2)
                    {
                        secondHit = true;
                    }
                    if (firstHit == true && secondHit == true)
                    {
                        bool endG;
                        pointTo(1);
                        playing = false;
                    }
                }
            }
        }
        updateScores_GUI();
    }

    void updateLastPlayerHit(int player)
    {
        if (player == 1)
        {
            lastPlayerHit = 1;
        }
        else
        {
            lastPlayerHit = 2;
        }

        updateScores_GUI();
    }

    void updateScores_GUI()
    {
        player1_score_text.text = "P1 gScore: " + player1_gameScore + ", P2 gScore: " + player2_gameScore + ", MatchInProg: " + matchInProgress + ", LastTHit: " + lastTableHit;
        player2_score_text.text = "P1 sScore: " + player1_setScore + ", P2 sScore: " + player2_setScore + ", Serving: " + isServing + ", First i sec hit: " + firstHit + " " + secondHit;
    }

    void checkServe(Collision other)
    {
        if (!firstHit && other.transform.CompareTag("Player1Table"))
        {
            firstHit = true;
            lastTableHit = 1;
        }

        if (firstHit && other.transform.CompareTag("Player2Table"))
        {
             secondHit = true;
             lastTableHit = 2;
        }
       
    }

    void pointTo(int player)
    {
        if (player == 1)
        {
            player1_gameScore++;
        }
        else if (player == 2)
        {
            player2_gameScore++;
        }
    }

    void endGame()
    {
        lastPlayerHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;
    }
}
