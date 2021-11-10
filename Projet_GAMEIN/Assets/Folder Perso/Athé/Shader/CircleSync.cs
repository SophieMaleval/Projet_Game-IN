using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSync : MonoBehaviour
{
    public static int SizeId = Shader.PropertyToID("_Size");

    public Material WallMaterial;

  



          void OnTriggerEnter2D (Collider2D other) {

             


       if (other.gameObject.tag == "Player") 
         {

             WallMaterial.SetFloat(SizeId, 0.5f); 
             Debug.Log ("dfg");
         }
  }

     void OnTriggerExit2D (Collider2D other) {
    
         if (other.gameObject.tag == "Player") 
         {

             WallMaterial.SetFloat(SizeId, 0f); 
             Debug.Log ("dfg");
         }


     }
  

    
    


         
    
}


