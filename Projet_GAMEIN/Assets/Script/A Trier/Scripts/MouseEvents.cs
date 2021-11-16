using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 
 public class MouseEvents : MonoBehaviour {
 
     public Camera mainCamera;
     
     // Update is called once per frame
     void Update () {
         RaycastHit2D hit;
 
         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
         if(hit = Physics2D.Raycast(ray.origin, new Vector2(0,0)))
             Debug.Log (hit.collider.name);
     }
 }