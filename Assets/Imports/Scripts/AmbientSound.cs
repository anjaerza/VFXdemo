using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    float t, limit;
    AudioSource sound;
    [SerializeField] bool fiya;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        if(fiya)
        {
            limit = 4 + GetComponentInParent<EffectEditor>().Anti + GetComponentInParent<EffectEditor>().Action + GetComponentInParent<EffectEditor>().ComAct;
        }
        else
        {
            limit = 2 + GetComponentInParent<EffectEditor>().ComAct;
        }
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
            sound.volume = GetComponentInParent<EffectEditor>().Curv.Evaluate(t / limit) * 10;

            t += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed;
        }
        else
        {
            sound.volume = 0;
        }
    }
}
