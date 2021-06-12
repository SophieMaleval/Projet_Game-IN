using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    [HideInInspector]
    public Vector3 velocity; // Calculated average velocity

    // Update is called once per frame
    void Update()
    {
        // Make the mouse cursor invisible
        Cursor.visible = false;

        // Get the mouse position (in screen coordinates)
        Vector2 mousePosition = Input.mousePosition;

        // Clamp that position to the screen
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);

        // Our next position will be that position mapped to the world by the main camera
        Vector3 nextPos = Camera.main.ScreenToWorldPoint(mousePosition) + Vector3.forward * 10;

        // Calculate our average velocity
        velocity = Vector3.Lerp(velocity, (nextPos - transform.position)/Time.deltaTime, 0.25f);

        // Move to the next position
        transform.position = nextPos;

        

        }
    }

