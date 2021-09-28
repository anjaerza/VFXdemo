using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControlEfecto : MonoBehaviour
{



    [SerializeField] Button button;

    [Range (1,3)]
    public float size;
    [Range (1,3)]
    public float velocidad;
    float duracion;

    public bool playing;

    [SerializeField]AudioMixer audioMixer;

    [SerializeField]Gradient gradA;
    [SerializeField]Gradient gradB;
    [SerializeField]AnimationCurve eControl;
    [SerializeField]Animator animator;
    [SerializeField]ObtenerCamara camara;
    [SerializeField]GameObject proyector;
    [SerializeField]Light light;
    float iLuz;


    [SerializeField]ParticleSystem triangle;
    const float LTRI=9;
    [SerializeField]ParticleSystem ring;
    [SerializeField]ParticleSystem inferno;
    [SerializeField]ParticleSystem souls;
    ParticleSystem.MinMaxCurve lSouls;
    ParticleSystem.MinMaxCurve sSouls;
    ParticleSystem.MinMaxCurve zSouls;
    

    [SerializeField]ParticleSystem fire;
    
    ParticleSystem.MinMaxCurve zFire;

    [SerializeField]ParticleSystem dist;
    ParticleSystem.MinMaxCurve zDist;


    [SerializeField]ParticleSystem humo;
    ParticleSystem.MinMaxCurve zHumo;
    [SerializeField]ParticleSystem gly;

    bool bBoom;
    bool bBass;
    bool bSouls;
    bool bInferno;
    bool bFire;


    float t;
    


    // Start is called before the first frame update
    void Start()
    {
        bBoom=false;
        bBass=false;
        bSouls=false;
        bInferno=false;
        bFire=false;

        button.enabled=true;


        var soMain = souls.main;
        lSouls=soMain.startLifetime;
        sSouls=soMain.startSpeed;
        zSouls=soMain.startSize;
        var fiSizeL=fire.sizeOverLifetime;
        zFire=fiSizeL.size;
        var diSizeL=dist.sizeOverLifetime;
        zDist=diSizeL.size;
        var huSizeL=humo.sizeOverLifetime;
        zHumo=huSizeL.size;

        proyector.SetActive(false);
        playing=false;
        animator.speed=velocidad;
        duracion=10/velocidad;
        t=0;
        animator.speed=1;

    }

    // Update is called once per frame
    void Update()
    {
        float vol = Mathf.Pow(1f+eControl.Evaluate(t/duracion),3f);
        audioMixer.SetFloat("masterVol",vol);
        audioMixer.SetFloat("masterPit",1*velocidad);



        transform.position  = new Vector3 (0.53f,20f*size, 0.96f);
        var infRenderer = inferno.gameObject.GetComponent<Renderer>();
        if(t/duracion >= 1)
        {
            playing=false;
            t=0;
            proyector.SetActive(false);
            animator.ResetTrigger("Comp");
            animator.speed=1;
            iLuz=0;

            bBoom=false;
            bBass=false;
            bSouls=false;
            bInferno=false;
            bFire=false;
            
            infRenderer.material.SetFloat("_FactorA",0.66f);

            button.enabled=true;

        }
        duracion=10/velocidad;
        if(Input.GetButtonDown("Jump"))
        {
            if(!playing)
            {
                gly.Play();
                animator.speed=velocidad;
                proyector.SetActive(true);
                playing=true;
                button.enabled=false;
                animator.SetTrigger("Anti");
                iLuz=6;
                gly.gameObject.GetComponent<AudioSource>().Play();
            }
            
        }

        if(playing)
        {
            t+=Time.deltaTime;
        }

        if(t/duracion > 0.2f)
        {
            animator.ResetTrigger("Anti");
            animator.SetTrigger("Acc");
        }

        if(t/duracion > 0.6f)
        {
            animator.ResetTrigger("Acc");
            animator.SetTrigger("Comp");
        }

        //Sonidos

        if(t/duracion > 0.1f && !bBass) //BassDrop
        {
            triangle.gameObject.GetComponent<AudioSource>().Play();
            bBass = true;
        }

        if(t/duracion > 0.20f && !bSouls) //Souls
        {
            souls.gameObject.GetComponent<AudioSource>().Play();
            bSouls=true;
        }

        if(t/duracion > 0.28f && !bInferno) //Inferno
        {
            inferno.gameObject.GetComponent<AudioSource>().Play();
            bInferno=true;
        }

        if(t/duracion > 0.5f && !bFire) //Fire
        {
            fire.gameObject.GetComponent<AudioSource>().Play();
            bFire=true;
        }

        if(t/duracion > 0.75f && !bBoom) //Humo
        {
            humo.gameObject.GetComponent<AudioSource>().Play();
            bBoom=true;
        }







        //Glifos

        var glyRenderer = gly.gameObject.GetComponent<Renderer>();
        glyRenderer.material.SetColor("_Color",ColorControl(gradB,eControl,t/duracion));
        glyRenderer.material.SetColor("_EmissionColor",ColorControl(gradB,eControl,t/duracion));
        var glyMain = gly.main;
        glyMain.startSize=1*size;
        glyMain.startLifetime=8/velocidad;
        var glyShape = gly.shape;
        glyShape.radius=7.63f*size;


        //Triangulo
        //var mtri=triangle.main;
        //mtri.startColor=ColorControl(gradB,eControl,t/duracion);
        var triRenderer = triangle.gameObject.GetComponent<Renderer>();
        triRenderer.material.SetColor("_Color",ColorControl(gradB,eControl,t/duracion));
        var triMain = triangle.main;
        triMain.startSize=1*size;
        triMain.startLifetime=LTRI/velocidad;

        //Ring
        var ringRenderer = ring.gameObject.GetComponent<Renderer>();
        ringRenderer.material.SetColor("_ColorE",ColorControl(gradA,eControl,t/duracion));
        var ringMain = ring.main;
        ringMain.startSize=1*size;
        ringMain.startLifetime=LTRI/velocidad;

        //Inferno
        infRenderer.material.SetColor("_Color",ColorControl(gradB,eControl,t/duracion));
        if(t/duracion > 0.6f)
        {
            infRenderer.material.SetFloat("_FactorA",4);
        }
        var infMain = inferno.main;
        infMain.startSize=1*size;
        infMain.startLifetime=LTRI/velocidad;

        //Souls

        var soRenderer = souls.gameObject.GetComponent<Renderer>();
        soRenderer.material.SetColor("_Color",ColorControl(gradA,eControl,t/duracion));
        var soMain = souls.main;
        soMain.startLifetime=lSouls.Evaluate(t/duracion);
        soMain.startSpeed=sSouls.Evaluate(t/duracion);
        soMain.startSize=zSouls.Evaluate(t/duracion)*size;
        var soShape = souls.shape;
        soShape.radius=16.35f*size;
        var soEmi=souls.emission;
        soEmi.rateOverTime=50*size;

        //Fire
        var fiRenderer = fire.gameObject.GetComponent<Renderer>();
        fiRenderer.material.SetColor("_Color",ColorControl(gradB,eControl,t/duracion));
        fiRenderer.material.SetColor("_EmissionColor",ColorControl(gradB,eControl,t/duracion));
        var fiSizeL = fire.sizeOverLifetime;
        fiSizeL.size=zFire.Evaluate(t/duracion);
        var fiEmi = fire.emission;
        fiEmi.rateOverTime=150*size;
        var fiShape = fire.shape;
        fiShape.radius=3.56f*size;

        //Distorsion
        var diRenderer = dist.gameObject.GetComponent<Renderer>();
        diRenderer.material.SetColor("_Color",ColorControl(gradA,eControl,t/duracion));
        var diSizeL = dist.sizeOverLifetime;
        diSizeL.size=zDist.Evaluate(t/duracion)*size;
        var diShape = dist.shape;
        diShape.radius=1f*size;
        var diMain = dist.main;
        

        //Humo
        var huRenderer = humo.gameObject.GetComponent<Renderer>();
        huRenderer.material.SetColor("_Color",ColorControl(gradB,eControl,t/duracion));
        var huSizeL = humo.sizeOverLifetime;
        huSizeL.size=zHumo.Evaluate(t/duracion)*size;
        var huMain = humo.main;
        huMain.startSize=5*size;

        //Proyector
        Projector projector= proyector.GetComponent<Projector>();
        projector.orthographicSize=13.8f*size;
        var prRenderer=projector.material;
        prRenderer.SetColor("_Color",new Color(gradA.Evaluate
        (t/duracion).r,gradA.Evaluate(t/duracion).g,gradA.Evaluate(t/duracion)
        .b,1));
        prRenderer.SetFloat("_Attenuation",eControl.Evaluate(t/duracion)*2);
        








        //Luz
        light.intensity=iLuz*eControl.Evaluate(t/duracion);
        light.color=gradA.Evaluate(t/duracion);

        //PostPro
        camara.GetComponent<ObtenerCamara>().factor=eControl.Evaluate(t/duracion)/2f;


    



    }

    Color ColorControl(Gradient g , AnimationCurve ac, float td)
    {
        Color color;
        color=new Color(g.Evaluate(td).r,g.Evaluate(td).g,g.Evaluate(td).b,ac.Evaluate(td));
        return color;
    }

    public void modificarSize(float nuevoValor)
    {
        size=nuevoValor;
    }

    public void modificarVel(float nuevoValor)
    {
        velocidad=nuevoValor;
    }
}
