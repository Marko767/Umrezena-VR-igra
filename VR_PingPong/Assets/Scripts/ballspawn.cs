using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballspawn : MonoBehaviour
{
    public Transform[] location;
    public GameObject prefab;
    public float spawntime;
    public float spawndelay;

    void Start()
    {
        InvokeRepeating("spawn", spawntime, spawndelay);
    }
    public void spawn()
    {
        Debug.Log("im here");
        int rand = Random.Range(0, 3);
        Debug.Log(rand);
        var clone = Instantiate(prefab, location[rand].position, location[rand].rotation);
        clone.GetComponent<Rigidbody>().velocity = new Vector3(-7.5f, 0, 0);
        Destroy(clone, 5);
    }

}
