using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPushable : MonoBehaviour
{  
    public float force = 3f;
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 dir = new Vector3((other.contacts[0].point.x), (other.contacts[0].point.y), 0) - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir*force);
        }

    }
}
