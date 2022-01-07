using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource ClickingSlider;
    // Start is called before the first frame update
    
    public void CLickingOnSLider(){

        ClickingSlider.Play();
    }
}
