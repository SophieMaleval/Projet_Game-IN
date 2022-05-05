using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }
}
