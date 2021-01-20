using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lefthandbutton : MonoBehaviour
{
    public string scene_name;
    public ballspawn spawner;
    public float spawntime;
    public float spawndelay;
    public Transform[] location;
    public GameObject prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("MenuButton")){
            SceneManager.LoadScene(scene_name);
        }

        if (other.transform.CompareTag("PlayButton"))
        {
            InvokeRepeating("spawn", spawntime, spawndelay);
        }
    }

    void spawn()
    {
        Debug.Log("im here");
        int rand = Random.Range(0, 3);
        Debug.Log(rand);
        var clone = Instantiate(prefab, location[rand].position, location[rand].rotation);
        clone.GetComponent<Rigidbody>().velocity = new Vector3(10, 0, 0);
    }
}
