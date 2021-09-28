using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] float gc, field, limit;
    Collider[] planets;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        planets = Physics.OverlapSphere(transform.position, field);
        rb = GetComponent<Rigidbody>();
        gc = gc * 6.67408f;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i  = 0; i < planets.Length; i++)
        {
            if(planets[i] != gameObject.GetComponent<Collider>())
            {
                if (Vector3.Distance(gameObject.transform.position, planets[i].transform.position) >= limit)
                {
                    planets[i].GetComponent<Rigidbody>().AddForce(Vector3.Normalize(-(planets[i].transform.position - gameObject.transform.position)) * (gc * ((transform.GetComponent<Rigidbody>().mass * planets[i].GetComponent<Rigidbody>().mass) / Vector3.Distance(gameObject.transform.position, planets[i].transform.position))));
                }
                else
                    planets[i].GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            }
        }
    }
}