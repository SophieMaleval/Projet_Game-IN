using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsController : MonoBehaviour
{
    public AudioSource Curtains;

    void PlayCurtainsSounds()
    {

        Curtains.Play();
    }
}
