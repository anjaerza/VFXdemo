using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Efecto : MonoBehaviour
{
    public AnimationCurve eControl;

    [Range (5,15)]
    public float duración;

    public AudioSource au;
    public Gradient g1;
    public Gradient g2;

    public abstract void Play();

}
