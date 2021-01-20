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
    int previousTableHit;

    bool gameInProgress;
    public static bool matchInProgress;

    int isServing;

    //servis
    bool firstHit;
    bool secondHit;

    public AudioSource hitSound;

    [SerializeField] Text player1_score_text;
    [SerializeField] Text player2_score_text;
    [SerializeField] Text player1_text;
    [SerializeField] Text player2_text;
    [SerializeField] Text dots;
    [SerializeField] Text end_text;
    [SerializeField] Text match_text;

    // Start is called before the first frame update
    void Start()
    {
        player1_gameScore = 0;
        player2_gameScore = 0;
        player1_setScore = 0;
        player2_setScore = 0;

        lastPlayerHit = 0;
        lastTableHit = 0;
        previousTableHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;
        matchInProgress = false;
        isServing = 1;

        hitSound = GetComponent<AudioSource>();

        end_text.text = "";
        player2_score_text.text = "";
        player1_score_text.text = "";
        dots.text = ":";
    }

    // Update is called once per frame
    void Update()
    {
        if (matchInProgress)
        {
            match_text.text = "Match in progress";
        }
        else
        {
            match_text.text = "Match not in progress";
        }

        updateScores_GUI();

        if (player1_gameScore == 11)
        {
            declareWinner(1);
            resetAll();
        }

        else if (player2_gameScore == 11)
        {
            declareWinner(2);
            resetAll();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("BluePaddle") || collision.transform.CompareTag("OrangePaddle") || collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Out"))
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

        if (lastPlayerHit != 0)
        {
            if (matchInProgress && playing)
            {
                if (collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Out"))
                {

                    if (lastPlayerHit == 1)
                    {
                        if (lastTableHit == 0 && collision.transform.CompareTag("Player1Table"))
                        {
                            previousTableHit = 1;
                        }

                        else if (collision.transform.CompareTag("Player2Table") && previousTableHit == 1)
                        {
                            gameInProgress = true;
                        }

                        else if (collision.transform.CompareTag("Player1Table") && previousTableHit == 1 && !gameInProgress)
                        {
                            pointTo(2);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out") && lastTableHit == 2)
                        {
                            pointTo(1);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out") && lastTableHit == 1)
                        {
                            pointTo(2);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out"))
                        {
                            pointTo(2);
                            playing = false;
                            endGame();
                        }
                    }

                    if (lastPlayerHit == 2)
                    {
                        if (lastTableHit == 0 && collision.transform.CompareTag("Player2Table"))
                        {
                            previousTableHit = 2;
                        }

                        else if (collision.transform.CompareTag("Player1Table") && previousTableHit == 2)
                        {
                            gameInProgress = true;
                        }

                        else if (collision.transform.CompareTag("Player2Table") && previousTableHit == 2 && !gameInProgress)
                        {
                            pointTo(1);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out") && lastTableHit == 1)
                        {
                            pointTo(2);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out") && lastTableHit == 2)
                        {
                            pointTo(1);
                            playing = false;
                            endGame();
                        }

                        else if (collision.transform.CompareTag("Out"))
                        {
                            pointTo(1);
                            playing = false;
                            endGame();
                        }
                    }

                }
            }
        }

        if (collision.transform.CompareTag("Player1Table"))
        {
            lastTableHit = 1;
        }
        else if (collision.transform.CompareTag("Player2Table"))
        {
            lastTableHit = 2;
        }
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
        end_text.text = "";
        player1_score_text.text = player1_gameScore.ToString();
        player2_score_text.text = player2_gameScore.ToString();
    }

    void checkServe()
    {
        if (lastPlayerHit == 1)
        {
            if (!firstHit && lastTableHit == 1)
            {
                firstHit = true;
            }
            else if (firstHit && lastTableHit == 2)
            {
                secondHit = true;
            }
        }
        else if (lastPlayerHit == 2)
        {
            if (!firstHit && lastTableHit == 2)
            {
                firstHit = true;
            }
            else if (firstHit && lastTableHit == 1)
            {
                secondHit = true;
            }
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
        lastTableHit = 0;
        previousTableHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;

        //switchServePlayer();
    }

    void declareWinner(int player)
    {
        player2_score_text.text = "";
        player1_score_text.text = "";
        player2_text.text = "";
        player1_text.text = "";
        dots.text = "";
        end_text.text = "Player " + player + " won!";
    }

    void resetAll()
    {
        player1_gameScore = 0;
        player2_gameScore = 0;
        player1_setScore = 0;
        player2_setScore = 0;

        lastPlayerHit = 0;
        lastTableHit = 0;
        previousTableHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;
        matchInProgress = false;
        isServing = 1;
    }

    void switchServePlayer()
    {
        if (player1_gameScore <= 9 && player2_gameScore <= 9)
        {
            if (((player1_gameScore + player2_gameScore) % 2) == 0)
            {
                if (isServing == 1)
                {
                    isServing = 2;
                }
                else
                {
                    isServing = 1;
                }
            }
        }

        else if (player1_gameScore >= 10 && player2_gameScore >= 10)
        {
            if (isServing == 1)
            {
                isServing = 2;
            }
            else
            {
                isServing = 1;
            }
        }
    }
}
