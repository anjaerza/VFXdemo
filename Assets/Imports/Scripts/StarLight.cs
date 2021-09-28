using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarLight : MonoBehaviour
{
    float t, limit;
    Light lite;

    // Start is called before the first frame update
    void Start()
    {
        lite = GetComponent<Light>();
        limit = GetComponentInParent<EffectEditor>().Anti + GetComponentInParent<EffectEditor>().Action;
    }
    private void OnEnable()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < limit)
        {
            lite.spotAngle = 40 + GetComponentInParent<EffectEditor>().Scale * 2;
            lite.range = 10* GetComponentInParent<EffectEditor>().Scale;

            lite.color = GetComponentInParent<EffectEditor>().Col.Evaluate(t / limit);
            lite.intensity = GetComponentInParent<EffectEditor>().Curv.Evaluate(t / limit) * 10;

            t += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed;
        }
        else
        {
            lite.intensity = 0;
        }
    }
}
