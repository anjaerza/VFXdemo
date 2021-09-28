using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParticleStart()
    {
        ps.Play();
    }

    public void AnimStart()
    {
    }
}
