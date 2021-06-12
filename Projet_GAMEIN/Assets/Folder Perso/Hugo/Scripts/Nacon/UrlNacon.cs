using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrlNacon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && Input.GetKeyDown("space"))
        {
            Application.OpenURL("http://unity3d.com/");
        }
    }
}
