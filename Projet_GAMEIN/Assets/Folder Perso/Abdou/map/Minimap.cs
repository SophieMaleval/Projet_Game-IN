using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform playerIcon;

    [Header("Minimap Cameras")]
    public GameObject mainCam;
    public GameObject mapCam;

    private bool mapActivated;

    // Update is called once per frame

    private void Start()
    {
        mapActivated = false;
    }
    void LateUpdate()
    {
        Vector3 newPosition = playerIcon.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapActivated = !mapActivated;
        }
        SwitchCam();
    }

    void SwitchCam()
    {
        if (mapActivated == true)
        {
            mainCam.SetActive(false);
            mapCam.SetActive(true);
        }
        if (mapActivated == false)
        {
            mainCam.SetActive(true);
            mapCam.SetActive(false);
        }
    }
}
