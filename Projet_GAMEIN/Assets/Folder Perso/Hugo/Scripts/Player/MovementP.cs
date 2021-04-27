using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementP : MonoBehaviour
{
    public float speed;
    public int direction;

    private float LastMX;
    private float LastMY;

    public bool isMoving;
    enum Direction
    {
        North,
        East,
        South,
        West
    }
    Direction CurrentDirection;
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isMoving = true;
        if(Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                movement.y = 0 ;
            else
                movement.x = 0;
        if(movement != Vector3.zero)
            {
                if(movement.x < 0){CurrentDirection = Direction.West;}
                if(movement.x > 0){CurrentDirection = Direction.East;}
                if(movement.y < 0){CurrentDirection = Direction.South;}
                if(movement.y > 0){CurrentDirection = Direction.North;}

                switch(CurrentDirection)
                {
                    case Direction.North:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = NorthSprite ;
                        gameObject.transform.localEulerAngles = new Vector3(0,0,0f);
                        break ;
                    case Direction.East:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = EastSprite ;
                        gameObject.transform.localEulerAngles = new Vector3(0,0,-90f);
                        break ;
                    case Direction.South:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = SouthSprite ;
                        gameObject.transform.localEulerAngles = new Vector3(0,0,180f);
                        break ;
                    case Direction.West:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = WestSprite ;
                        gameObject.transform.localEulerAngles = new Vector3(0,0,90f);
                        break ;
                }
            }
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            LastMX  = Input.GetAxis("Horizontal");
            LastMY = Input.GetAxis("Vertical");
        }

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            isMoving = false;
        }
        transform.position += movement * speed * Time.deltaTime;
    }
    /*void OnCollisionExit2D(Collision2D colExt)
    {
        if (colExt.gameObject.tag == "Pushable Object")
        colExt.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }*/
}
