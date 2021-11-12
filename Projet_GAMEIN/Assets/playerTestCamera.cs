using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerTestCamera : MonoBehaviour
{

    public string level;
    public SceneManagementController Sm;

    void start()
    {
        Sm.shouldReveal = true;
    }

   
    // Update is called once per frame
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        
        {
            Sm.shouldReveal = false;
        }

   
      

         
    }
    
        
    
}
