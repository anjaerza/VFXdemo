using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0), GetComponentInParent<EffectEditor>().Speed * 4);

        if(transform.localScale.x <= 2)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), Time.deltaTime * (GetComponentInParent<EffectEditor>().Speed));
        }
    }
}
