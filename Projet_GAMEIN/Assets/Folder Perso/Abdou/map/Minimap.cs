using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform playerIcon;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = playerIcon.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
