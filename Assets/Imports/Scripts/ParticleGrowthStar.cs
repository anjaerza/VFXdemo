using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ParticleGrowthStar : MonoBehaviour
{
    float t, limit;
    ParticleSystem ps;
    ParticleSystem.EmissionModule emi;
    ParticleSystemRenderer pr;

    // Start is called before the first frame update
    void Start()
    {
        limit = GetComponentInParent<EffectEditor>().Anti + GetComponentInParent<EffectEditor>().Action;
        ps = GetComponent<ParticleSystem>();
        emi = ps.emission;
        pr = GetComponent<ParticleSystemRenderer>(); 
    }

    private void OnEnable()
    {
        t = 0;
        gameObject.transform.localScale = new Vector3(0, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (t < limit)
        {
            if (transform.localScale.x != 1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * (GetComponentInParent<EffectEditor>().Speed));
            }

            pr.sharedMaterial.color = GetComponentInParent<EffectEditor>().Col.Evaluate(t/limit);
            emi.rateOverTime = GetComponentInParent<EffectEditor>().Scale * 100;

            t += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed;
        }
        else
        {
            emi.rateOverTime = 0;
        }
    }
}