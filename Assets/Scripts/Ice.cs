using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice : MonoBehaviour
{
    ParticleSystem ps;
    bool occupied;
    BoxCollider col;
    [SerializeField] Gradient grad, gradBar;
    SpriteRenderer area;
    float time, limit;
    Slider bar;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        occupied = false;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
        area = GetComponentInChildren<SpriteRenderer>();
        bar = GetComponentInChildren<Slider>();
        time = 0;
        limit = 3;
        area.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(occupied)
        {
            time += Time.deltaTime;
            if(time <= 0.9f)
            {
                bar.value = time / 0.9f;
            }
            else
            {
                bar.value = 1 - ((time - 0.9f)/2.1f);
            }
        }
        if (time == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ps.Play();
                col.enabled = true;
                area.enabled = true;
                occupied = true;
            }
        }
        else
        {
            area.color = grad.Evaluate(time / limit);
            bar.GetComponentInChildren<Image>().color = gradBar.Evaluate(time / limit);
        }

        if (time >= limit)
        {
            time = 0;
            col.enabled = false;
            area.enabled = false;
            occupied = false;
        }
    }
}
