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
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            transform.position = new Vector2(transform.position.x + (x * Time.deltaTime * 5f), transform.position.y + 0);
        }
        else
            transform.position = new Vector2(transform.position.x + 0, transform.position.y + (y * Time.deltaTime * 5f));
    }






}
