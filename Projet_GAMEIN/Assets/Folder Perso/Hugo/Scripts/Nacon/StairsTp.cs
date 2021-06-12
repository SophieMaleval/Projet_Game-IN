using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsTp : MonoBehaviour
{
    public GameObject tpLocation;
    public bool canTp = false;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            canTp = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            canTp = false;
        }
    }

    public void Update() 
    {
        if(canTp == true /*&& Input.GetKeyDown(KeyCode.A)*/)
        {
            GameObject.Find("Player").transform.position = tpLocation.transform.position;
            Debug.Log("Allo");
        }
    }
}
