using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EffectEditor : MonoBehaviour
{
    [SerializeField] float scale, speed, anti, action, comAct;
    [SerializeField] Gradient col;
    [SerializeField] AnimationCurve curv;
    [SerializeField] GameObject antiObj, actionObj, comActObj, canvas, cam, ambAud, sumAud, statAud;
    bool active;

    public float Scale { get => scale; set => scale = value; }
    public float Speed { get => speed; set => speed = value; }
    public Gradient Col { get => col; set => col = value; }
    public AnimationCurve Curv { get => curv; set => curv = value; }
    public float Anti { get => anti; set => anti = value; }
    public float Action { get => action; set => action = value; }
    public float ComAct { get => comAct; set => comAct = value; }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                canvas.SetActive(false);
                active = true;
                StartCoroutine("Effect1");
            }
        }
        transform.localScale = new Vector3(scale, scale, scale);
    }

    IEnumerator Effect1()
    {

        actionObj.GetComponentInChildren<Animator>().SetBool("Active", true);
        yield return new WaitForSecondsRealtime(1 + action / (speed * 5));
        actionObj.GetComponentInChildren<Animator>().SetBool("Active", false);
        ambAud.GetComponent<AmbientSound>().enabled = true;
        for (int i = 0; i < antiObj.GetComponentsInChildren<ParticleSystem>().Length; i++)
        {
            antiObj.GetComponentsInChildren<ParticleSystem>()[i].Play();
        }
        for (int i = 0; i < antiObj.GetComponentsInChildren<ParticleGrowthStar>().Length; i++)
        {
            antiObj.GetComponentsInChildren<ParticleGrowthStar>()[i].enabled = true;
        }
        for (int i = 0; i < antiObj.GetComponentsInChildren<Runes>().Length; i++)
        {
            antiObj.GetComponentsInChildren<Runes>()[i].enabled = true;
        }
        antiObj.GetComponentInChildren<StarLight>().enabled = true;
        sumAud.GetComponent<AudioSource>().Play();
        yield return new WaitForSecondsRealtime(1 + anti / speed);
        actionObj.GetComponentInChildren<CharSummon>().enabled = true;
        yield return new WaitForSecondsRealtime(1 + (action*4) / (speed * 5));
        actionObj.GetComponentInChildren<Animator>().SetBool("Active", true);
        yield return new WaitForSecondsRealtime(1 + action / (speed * 5));
        actionObj.GetComponentInChildren<Animator>().SetBool("Active", false);

        for (int i = 0; i < antiObj.GetComponentsInChildren<ParticleGrowthStar>().Length; i++)
        {
            antiObj.GetComponentsInChildren<ParticleGrowthStar>()[i].enabled = false;
        }
        for (int i = 0; i < antiObj.GetComponentsInChildren<Runes>().Length; i++)
        {
            antiObj.GetComponentsInChildren<Runes>()[i].enabled = false;
        }
        antiObj.GetComponentInChildren<StarLight>().enabled = false;
        comActObj.GetComponentInChildren<GrowingCircle>().enabled = true;
        for (int i = 0; i < comActObj.GetComponentsInChildren<ParticleSystem>().Length; i++)
        {
            comActObj.GetComponentsInChildren<ParticleSystem>()[i].Play();
        }
        for (int i = 0; i < comActObj.GetComponentsInChildren<PartsComp>().Length; i++)
        {
            comActObj.GetComponentsInChildren<PartsComp>()[i].enabled = true;
        }
        comActObj.GetComponentInChildren<ProjectorHandle>().enabled = true;
        statAud.GetComponent<AmbientSound>().enabled = true;
        cam.GetComponent<CamEffector>().enabled = true;
        yield return new WaitForSecondsRealtime(1 + (comAct) / (speed));
        actionObj.GetComponentInChildren<CharSummon>().enabled = false;
        comActObj.GetComponentInChildren<ProjectorHandle>().enabled = false;
        for (int i = 0; i < comActObj.GetComponentsInChildren<PartsComp>().Length; i++)
        {
            comActObj.GetComponentsInChildren<PartsComp>()[i].enabled = false;
        }
        comActObj.GetComponentInChildren<GrowingCircle>().enabled = false;
        yield return new WaitForSecondsRealtime(1);
        ambAud.GetComponent<AmbientSound>().enabled = false;
        cam.GetComponent<CamEffector>().enabled = false;
        statAud.GetComponent<AmbientSound>().enabled = false;
        canvas.SetActive(true);
        active = false;
    }
}