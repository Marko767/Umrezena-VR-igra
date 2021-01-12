using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    bool playing = true;

    private float time;

    //game bodovi za svakog igraca
    int player1_gameScore;
    int player2_gameScore;

    //set bodovi
    int player1_setScore;
    int player2_setScore;

    //igrac koji je zadnji udario lopticu
    int lastPlayerHit;

    //dio stola u koji je loptica zadnje lupila (true za player1Table, false za player2Table)
    int lastTableHit;

    bool gameInProgress;
    public bool matchInProgress;

    //true za player1, false za player2
    int isServing;

    //servis
    bool firstHit;
    bool secondHit;

    //ne radi, je li igrac u pravilnom podrucju za servis (iza stola)
    public bool playerInRightArea;

    public AudioSource hitSound;

    [SerializeField] Transform servePlayer1;
    [SerializeField] Transform servePlayer2;

    [SerializeField] Text player1_score_text;
    [SerializeField] Text player2_score_text;

    // Start is called before the first frame update
    void Start()
    {
        //time = 0.0f;

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

        updateScores_GUI();
    }

    private void Update()
    {
       // time = Time.captureDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //play zvuk kod kolizije lopte i objekta na sceni
        if(collision.transform.CompareTag("BluePaddle") || collision.transform.CompareTag("OrangePaddle") || collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Player1ServeArea") || collision.transform.CompareTag("Player1TableArea") || collision.transform.CompareTag("Player2ServeArea") || collision.transform.CompareTag("Player2TableArea") || collision.transform.CompareTag("Walls"))
        {
            hitSound.Play();
        }

        //svaki igrac ima svoj reket
        //update igraca koji je zadnji lupio loptu
        //igra (game) ne pocne dok reket ne udari lopticu, odnosno ne boduje se ako igrac baci ili ispusti lotpicu
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

        // PRVI IGRAC = PLAVI IGRAC
        // DRUGI IGRAC = NARANCASTI IGRAC

        //pocetak igre
        if (lastPlayerHit != 0)
        {
            //ako je mec poceo i loptica je udarena reketom
            if (matchInProgress && playing)
            {
                //ako je collide s objektom koji nije reket, a sudjeluje u bodovanju
                if(collision.transform.CompareTag("Player1Table") || collision.transform.CompareTag("Player2Table") || collision.transform.CompareTag("Player1ServeArea") || collision.transform.CompareTag("Player1TableArea") || collision.transform.CompareTag("Player2ServeArea") || collision.transform.CompareTag("Player2TableArea") || collision.transform.CompareTag("Walls"))
                {
                    //provjeri je li loptica pala na pravo mjesto prilikom servisa
                    if(gameInProgress == false) checkServe(collision);

                   /* if (time > 0.3f)
                    {

                        time = 0.0f;*/

                        //ako je prvi igrac servirao

                            //loptica je dvaput pala na pravo mjesto ------ servis je dobar i pocinje igra
                            if (firstHit == true && secondHit == true)
                            {
                                bool endG;
                                //gameInProgress = true;
                                pointTo(1);
                                //provjeri je li game gotov, ako je zavrsi game
                                //endG = checkGameEnd(collision);
                                //if (endG)
                                //{
                                playing = false;
                                endGame();
                                //}
                            }

                            //prvi udarac je dobar, drugi je otisao u aut ------ servis nije dobar
                            else if (firstHit == true && secondHit == false && !collision.transform.CompareTag("Player1Table"))
                            {
                                pointTo(2);
                                playing = false;
                                //endGame();
                            }

                            //prvi udarac je dobar, drugi je otisao u mrezu ili je dvaput lupila na njegovu polovicu ------ servis nije dobar
                            else if (firstHit == true && secondHit == false && collision.transform.CompareTag("Player1Table") && lastTableHit == 1)
                            {
                                pointTo(2);
                                playing = false;
                                //endGame();
                            }

                            //prvi udarac nije dobar ------ servis nije dobar
                            else if (firstHit == false && secondHit == false)
                            {
                                pointTo(2);
                                playing = false;
                                //endGame();
                            }
                        }

                        //isto za drugog
                       /* else if (isServing == false)
                        {
                            if (firstHit == true && secondHit == true)
                            {
                                bool endG;
                                //gameInProgress = true;
                                pointTo(2);
                                //endG = checkGameEnd(collision);
                                //if (endG)
                                //{
                                playing = false;
                                endGame();
                                //}
                            }

                            else if (firstHit == true && secondHit == false && !collision.transform.CompareTag("Player2Table"))
                            {
                                pointTo(1);
                                playing = false;
                                endGame();
                            }

                            else if (firstHit == true && secondHit == false && collision.transform.CompareTag("Player2Table") && lastTableHit == false)
                            {
                                pointTo(1);
                                playing = false;
                                endGame();
                            }

                            else if (firstHit == false && secondHit == false)
                            {
                                pointTo(1);
                                playing = false;
                                endGame();
                            }
                        }
                   //}*/

                //}

                //provjeri treba li zamijeniti igraca koji servira
                switchServePlayer();

                bool endS;
                endS = checkSetEnd();

                if (endS)
                {
                    endSet();
                }

                //provjera je li mec gotov (ako neki igrac osvoji 3 seta)
                if (player1_setScore == 1)
                {
                    declareWinner(1);
                    resetAll();
                }

                else if(player2_setScore == 1)
                {
                    declareWinner(2);
                    resetAll();
                }

            }
        }

        updateScores_GUI();
    }

    void checkServe(Collision other)
    {
        //ako prvi igrac servira
       // if (isServing == true)
      //  {
            //prvi udarac u stol, mora biti u njegov dio stola
            if (!firstHit && other.transform.CompareTag("Player1Table"))
            {
                firstHit = true;
                lastTableHit = 1;
            }

            //drugi udarac u stol, mora biti u protivnikov dio stola (i prethodno je morala lupiti u njegov dio)
            //servis je tada ok
            if (firstHit && other.transform.CompareTag("Player2Table"))
            {
                secondHit = true;
                lastTableHit = 2;
            }
        //}
        /*
        //isto za drugog igraca
        else if (isServing == false)
        {
            if (!firstHit && other.transform.CompareTag("Player2Table"))
            {
                firstHit = true;
            }

            if (firstHit && other.transform.CompareTag("Player1Table"))
            {
                secondHit = true;
            }
        }

        updateScores_GUI();*/
    }
    public void updateScores_GUI()
    {
        player1_score_text.text = "P1 gScore: " + player1_gameScore + ", P2 gScore: " + player2_gameScore + ", MatchInProg: " + matchInProgress + ", LastTHit: " + lastTableHit;
        player2_score_text.text = "P1 sScore: " + player1_setScore + ", P2 sScore: " + player2_setScore + ", Serving: " + isServing + ", First i sec hit: " + firstHit + " " + secondHit;
    }

    void pointTo(int player)
    {
        if(player == 1)
        {
            player1_gameScore++;
        }
        else if(player == 2)
        {
            player2_gameScore++;
        }
    }

    void updateLastPlayerHit(int player)
    {
        if(player == 1)
        {
            lastPlayerHit = 1;
        }
        else
        {
            lastPlayerHit = 2;
        }

        updateScores_GUI();
    }


    bool checkGameEnd(Collision other)
    {
        bool end = false;

        if (other.transform.CompareTag("Player1Table"))
        {
            //loptica je lupila dvaput ili u mrezicu pa opet u njegov dio stola
            if(lastTableHit == 1)
            {
                pointTo(2);
                end = true;
            }

            lastTableHit = 1;

            //igrac gubi ako ju je udario i ako je loptica lupila u njegov dio stola
            if (lastPlayerHit == 1)
            {
                pointTo(2);
                end = true;
            }
        }
        else if (other.transform.CompareTag("Player2Table"))
        {
            if (lastTableHit == 2)
            {
                pointTo(1);
                end = true;
            }

            lastTableHit = 2;

            if (lastPlayerHit == 2)
            {
                pointTo(1);
                end = true;
            }
        }


        //ako ode u aut, bod osvaja igrac koji nije zadnji lupio lopticu
        if (other.transform.CompareTag("Player1ServeArea") || other.transform.CompareTag("Player1TableArea") || other.transform.CompareTag("Player2ServeArea") || other.transform.CompareTag("Player2TableArea") || other.transform.CompareTag("Walls"))
        {
            if (lastPlayerHit == 1)
            {
                pointTo(2);
                end = true;
            }
            else
            {
                pointTo(1);
                end = true;
            }
        }

        //drugi slucajevi (ako lupi u njegov pa u suparnicki dio stola) su u redu, game se nastavlja

        return end;
    }

    bool checkSetEnd()
    {
        bool end;

        if (player1_gameScore == 11 && (player1_gameScore - player2_gameScore) >= 2)
        {
           player1_setScore++;
           end = true;
           updateScores_GUI();
        }

        else if (player2_gameScore == 11 && (player2_gameScore - player1_gameScore) >= 2)
        {
            player2_setScore++;
            end = true;
            updateScores_GUI();
        }

        else if (player1_gameScore > 11 && (player1_gameScore - player2_gameScore) >= 2)
        {
            player1_setScore++;
            end = true;
            updateScores_GUI();
        }

        else if (player2_gameScore > 11 && (player2_gameScore - player1_gameScore) >= 2)
        {
            player2_setScore++;
            end = true;
            updateScores_GUI();
        }

        else
        {
            end = false;
        }

        return end;
    }

    void switchServePlayer()
    {
        //ako oba igraca imaju manje od 9 bodova, mijenjaj igraca svaka dva servisa
        if(player1_gameScore <= 9 && player2_gameScore <= 9)
        {
            if(((player1_gameScore + player2_gameScore) % 2) == 0)
            {
                if(isServing == 1)
                {
                    isServing = 2;
                }
                else
                {
                    isServing = 1;
                }
            }
        }

        //inace nakon svakog servisa
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

    void endGame()
    {
        lastPlayerHit = 0;

        firstHit = false;
        secondHit = false;

        /*if(isServing == true)
        {
            transform.position = servePlayer1.position;
        }
        else transform.position = servePlayer2.position;*/
    }

    void endSet()
    {
        player1_gameScore = 0;
        player2_gameScore = 0;
        lastPlayerHit = 0;

        firstHit = false;
        secondHit = false;

        if (isServing == 1)
        {
            isServing = 2;
        }
        else isServing = 1;
    }

    void declareWinner(int player)
    {
        player1_score_text.text = "Winner is player " + player;
        player2_score_text.text = "";
    }

    void resetAll()
    {
        player1_gameScore = 0;
        player2_gameScore = 0;
        player1_setScore = 0;
        player2_setScore = 0;

        lastPlayerHit = 0;

        firstHit = false;
        secondHit = false;

        gameInProgress = false;
        matchInProgress = false;
        isServing = 1;
    }

    public void startMatch()
    {
        matchInProgress = true;
        updateScores_GUI();
    }
}
