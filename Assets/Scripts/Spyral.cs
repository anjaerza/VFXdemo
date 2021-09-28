using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyral : MonoBehaviour
{
    [SerializeField] float vy0, r0, w;
    [SerializeField] int spyrall;
    float t, x, y, z, r;
    // Start is called before the first frame update
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(spyrall == 0)
        {
            r = r0;
            x = r * Mathf.Cos(w * t);
            z = r * Mathf.Sin(w * t);
            y = t * vy0;
            transform.position = new Vector3(x, y, z);
        }
        else if (spyrall == 1)
        {
            r = t * r0;
            x = r * Mathf.Cos(w * t);
            z = r * Mathf.Sin(w * t);
            y = t * vy0;
            transform.position = new Vector3(x, y, z);
        }
        else if (spyrall == 2)
        {
            r = t * r0 * r0;
            x = r * Mathf.Cos(w * t);
            z = r * Mathf.Sin(w * t);
            y = t * vy0;
            transform.position = new Vector3(x, y, z);
        }
        else if (spyrall == 3)
        {
            r = r0 / Mathf.Sqrt(w * t);
            x = r * Mathf.Cos(w * t);
            z = r * Mathf.Sin(w * t);
            y = t * vy0;
            transform.position = new Vector3(x, y, z);
        }
    }
}