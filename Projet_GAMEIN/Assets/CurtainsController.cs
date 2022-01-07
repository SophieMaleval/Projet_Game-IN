using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    public AudioSource Curtains;
    public AudioSource CurtainsClosing;

    void PlayCurtainsSoundsOpening()
    {

        Curtains.Play();
    }
    void PlayCurtainsSoundsClosing()
    {

        CurtainsClosing.Play();
    }
}
