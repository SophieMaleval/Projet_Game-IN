using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public MainManager MainSceneManager ;
    public string sceneToLoad;
    // Start is called before the first frame update
   void OnTriggerEnter2D(Collider2D other) 
   {
        if(other.tag == "Player")
            {
                //SceneManager.LoadScene(sceneToLoad);
                MainSceneManager.GoToNewScene(sceneToLoad);
            }   
   }
}
