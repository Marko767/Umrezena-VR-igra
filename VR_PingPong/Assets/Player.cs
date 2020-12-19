using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 playerPosition;
    public static bool playerInRightArea;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1ServeArea") || other.CompareTag("Player2ServeArea"))
        {
            playerInRightArea = true;
        }
    }
}
