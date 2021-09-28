using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldS : MonoBehaviour
{
    [SerializeField] AnimationCurve curv;
    [SerializeField] Gradient grad;
    Light luz;
    ParticleSystem ps;
    AudioSource aud;
    float time, limit;
    bool activeShield;

    void Start()
    {
        luz = GetComponentInChildren<Light>();
        ps = GetComponentInChildren<ParticleSystem>();
        aud = GetComponentInChildren<AudioSource>();
        time = 0;
        limit = 6;
        activeShield = false;
    }

    void Update()
    {
        if (activeShield)
        {
            time += Time.deltaTime;
            luz.color = grad.Evaluate(time / limit);
            luz.intensity = curv.Evaluate(time / limit);
            aud.volume = curv.Evaluate(time / limit)/2;
            if(curv.Evaluate(time / limit) > 0.5f)
            {
                GetComponent<SphereCollider>().enabled = true;
            }
            else
            {
                GetComponent<SphereCollider>().enabled = false;
            }
        }

        if (time == 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                activeShield = true;
                ps.Play();
                aud.Play();
            }
        }
        else if (time >= limit)
        {
            activeShield = false;
            time = 0;
            aud.Stop();
            luz.intensity = 0;
        }
    }
}