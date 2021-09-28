using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] Gradient grad;
    [SerializeField] float vel;
    [SerializeField] ParticleSystem ps;
    Animator anim;
    bool occupied;
    Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        occupied = false;
        pos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, -0.02f, transform.position.z);

        if (!occupied)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.position += new Vector3(vel / 100, 0, 0);
                transform.position += (transform.forward * vel) / 100;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y+90, 0), 120f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.position += new Vector3(-vel / 100, 0, 0);
                transform.position += (transform.forward * vel) / 100;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y - 90, 0), 120f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //transform.position += new Vector3(0, 0, vel / 100);
                transform.position += (transform.forward * vel) / 100;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //transform.position += new Vector3(0, 0, -vel / 100);
                transform.position += (-transform.forward * vel) / 100;
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y+180, 0), 120f * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("Wait");
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine("Wait");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine("Wait2");
            }

            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) && !occupied)
            {
                anim.SetInteger("State", 1);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                anim.SetInteger("State", 2);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                anim.SetInteger("State", 3);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetInteger("State", 4);
                ps.Play();
            }
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetInteger("State", 0);
        }
    }

    IEnumerator Wait()
    {
        occupied = true;
        yield return new WaitForSecondsRealtime(3);
        occupied = false;
    }

    IEnumerator Wait2()
    {
        occupied = true;
        yield return new WaitForSecondsRealtime(0.18f);
        Transform pos = ps.transform;
        ps.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        yield return new WaitForSecondsRealtime(3);
        ps.transform.position = new Vector3(0.355f, 0.921f, 0.325f);
        occupied = false;
    }
}