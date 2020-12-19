using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMatchConfig : MonoBehaviour
{
    public static bool matchInProgress;
    public static int isServing;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        isServing = 1;
        matchInProgress = false;
        ball = gameObject.GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startMatch()
    {
        matchInProgress = true;
        ball.updateScores_GUI();
    }

    public void endMatch()
    {
        matchInProgress = false;
    }

    public void setPlayer1Serve()
    {
        isServing = 1;
    }

    public void setPlayer2Serve()
    {
        isServing = 2;
    }
}
