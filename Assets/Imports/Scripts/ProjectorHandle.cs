using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorHandle : MonoBehaviour
{
    float t, limit;
    bool done, smol;
    Projector proj;

    // Start is called before the first frame update
    void Start()
    {
        smol = true;
        done = false;
        proj = GetComponent<Projector>();
        limit = GetComponentInParent<EffectEditor>().ComAct;
    }

    private void OnEnable()
    {
        done = false;
        t = 0;
        GetComponent<Projector>().fieldOfView = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (t < limit)
        {
            if(!done)
            {
                if (proj.fieldOfView <= 100)
                {
                    proj.fieldOfView += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed * 100;
                }
                else
                {
                    done = true;
                    smol = false;
                }
            }
            else if(t >= limit/2)
            {
                if (!smol)
                {
                    if (proj.fieldOfView > 0)
                    {
                        proj.fieldOfView -= Time.deltaTime * GetComponentInParent<EffectEditor>().Speed * 300;
                    }
                    else
                    {
                        proj.fieldOfView = 0;
                        smol = true;
                    }
                }
            }
            

            t += Time.deltaTime * GetComponentInParent<EffectEditor>().Speed;
        }
        else
        {
            proj.fieldOfView = 0;
        }
    }
}
