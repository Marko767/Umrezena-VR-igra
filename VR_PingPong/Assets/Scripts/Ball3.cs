using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball3 : MonoBehaviour
{
    public AudioSource hitSound;


    // Start is called before the first frame update
    void Start()
    {
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

        
    }


}


