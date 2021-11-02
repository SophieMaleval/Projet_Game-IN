using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pas : MonoBehaviour
{
    public AudioSource Paspied;
    public AudioSource Paspied2;
    // Start is called before the first frame update
   public void Pas(){

       Paspied.Play();
   }

      public void Pas2(){

       Paspied2.Play();
   }
}
