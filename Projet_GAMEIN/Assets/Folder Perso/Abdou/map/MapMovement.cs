using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    private float x, y;
    public float moveSpeed;
    private bool isWalking;

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if(x != 0 || y != 0)
        {
            isWalking = true;

            Move();
        }

        else
        {
            if (isWalking)
            {
                isWalking = false;
            }
        }
    }

    private void Move()
    {
        transform.Translate(x * Time.deltaTime * moveSpeed, y * Time.deltaTime * moveSpeed, 0);
    }






}
