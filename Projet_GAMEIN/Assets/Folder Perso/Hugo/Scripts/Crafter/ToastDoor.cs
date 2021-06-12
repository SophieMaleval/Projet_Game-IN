using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastDoor : MonoBehaviour
{
    public GameObject door;
    public Vector3 startPos;
    public Vector3 endPos;
  
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Lerp");
        Vector3 lerpPos = Vector3.Lerp(startPos, endPos, 2f);
    }
}
