using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSummon : MonoBehaviour
{
    float t, limit, speed;
    bool summoned;

    void Start()
    {
        summoned = false;
        limit = GetComponentInParent<EffectEditor>().ComAct + GetComponentInParent<EffectEditor>().Action;
    }

    private void OnEnable()
    {
        t = 0;
        speed = GetComponentInParent<EffectEditor>().Speed;
        GetComponentInChildren<Renderer>().material.SetFloat("_Cut", GetComponentInParent<EffectEditor>().Scale * 2);
    }

    void Update()
    {
        if (t < limit)
        {
            if(!summoned)
            {
                if (t < limit - GetComponentInParent<EffectEditor>().Action)
                {
                    if (transform.position.y != 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(0,0, 0.11f), Time.deltaTime * (GetComponentInParent<EffectEditor>().Speed * GetComponentInParent<EffectEditor>().Scale));
                    }
                }
                else if (GetComponentInChildren<Renderer>().material.GetFloat("_Limits") < 1)
                {
                    GetComponentInChildren<Renderer>().material.SetFloat("_Limits", GetComponentInChildren<Renderer>().material.GetFloat("_Limits") + Time.deltaTime * GetComponentInParent<EffectEditor>().Scale * GetComponentInParent<EffectEditor>().Speed);
                }
            }
            else
            {
                if (t < (limit - GetComponentInParent<EffectEditor>().Action)/2)
                {
                    if (GetComponentInChildren<Renderer>().material.GetFloat("_Limits") > 0)
                    {
                        GetComponentInChildren<Renderer>().material.SetFloat("_Limits", GetComponentInChildren<Renderer>().material.GetFloat("_Limits") - Time.deltaTime * GetComponentInParent<EffectEditor>().Scale * GetComponentInParent<EffectEditor>().Speed);
                    }
                }
                else if (transform.position.y != -GetComponentInParent<EffectEditor>().Scale * 2.5f - 0.2f)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(0, -(GetComponentInParent<EffectEditor>().Scale*2.5f) - 0.4f, 0.11f), Time.deltaTime * (GetComponentInParent<EffectEditor>().Speed * GetComponentInParent<EffectEditor>().Scale));
                }
            }

            t += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed;
            if(t >= limit)
            {
                summoned = !summoned;
            }
        }
    }
}