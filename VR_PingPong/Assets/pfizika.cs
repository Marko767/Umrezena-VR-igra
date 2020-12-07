using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pfizika : MonoBehaviour
{
    private Rigidbody rigbody;
    private Vector3 brzina;
    public float sens = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rigbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dest = this.transform.position;
        rigbody.transform.rotation = transform.rotation;
        brzina = (dest - rigbody.transform.position) * sens;
        rigbody.velocity = brzina;
        //uzmi poziciju ovog trenutka
        //izračunaj (trenutna - prijašnja) pozicija
        //izračunaj kao vektor smjera
        //predaj taj vektor loptici
        //prijašnja pozicija = trenutna pozicija

    }
}
