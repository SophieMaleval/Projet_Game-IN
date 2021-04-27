using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDeplacement : MonoBehaviour
{
    [Header ("Visual")]
    public GameObject DosSprites ;
    public GameObject DroiteSprites ;
    public GameObject FaceSprites ;
    public GameObject GaucheSprites ;


    Direction CurrentDirection ;
    Vector2 InputPlayer ;
    public bool IsMoving = false ;
    Vector3 StartPos ;
    Vector3 Endpos ;
    float T ;

    [Header ("Walk Setting")]
    public float WalkSpeed = 3f ;

    void Update() 
    {
        if(!IsMoving)
        {
            InputPlayer = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(Mathf.Abs(InputPlayer.x) > Mathf.Abs(InputPlayer.y))
                InputPlayer.y = 0 ;
            else
                InputPlayer.x = 0;

            if(InputPlayer != Vector2.zero)
            {
                if(InputPlayer.x < 0)
                {
                    CurrentDirection = Direction.West ;
                }
                if(InputPlayer.x > 0)
                {
                    CurrentDirection = Direction.East ;
                }
                if(InputPlayer.y < 0)
                {
                    CurrentDirection = Direction.South ;
                }
                if(InputPlayer.y > 0)
                {
                    CurrentDirection = Direction.North ;
                }

                switch(CurrentDirection)
                {
                    case Direction.North:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = NorthSprite ;
                        //gameObject.transform.localEulerAngles = new Vector3(0,0,0f);
                        DosSprites.gameObject.SetActive(true);
                        DroiteSprites.gameObject.SetActive(false);
                        FaceSprites.gameObject.SetActive(false);
                        GaucheSprites.gameObject.SetActive(false); 
                        break ;
                    case Direction.East:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = EastSprite ;
                        //gameObject.transform.localEulerAngles = new Vector3(0,0,-90f);
                        DosSprites.gameObject.SetActive(false);
                        DroiteSprites.gameObject.SetActive(true);
                        FaceSprites.gameObject.SetActive(false);
                        GaucheSprites.gameObject.SetActive(false); 
                        break ;
                    case Direction.South:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = SouthSprite ;
                        //gameObject.transform.localEulerAngles = new Vector3(0,0,180f);
                        DosSprites.gameObject.SetActive(false);
                        DroiteSprites.gameObject.SetActive(false);
                        FaceSprites.gameObject.SetActive(true);
                        GaucheSprites.gameObject.SetActive(false);
                        break ;
                    case Direction.West:
                        //gameObject.GetComponent<SpriteRenderer>().sprite = WestSprite ;
                        //gameObject.transform.localEulerAngles = new Vector3(0,0,90f);
                        DosSprites.gameObject.SetActive(false);
                        DroiteSprites.gameObject.SetActive(false);
                        FaceSprites.gameObject.SetActive(false);
                        GaucheSprites.gameObject.SetActive(true);      
                        break ;
                }

                StartCoroutine(Move(transform));
            }
        }
    }

    public IEnumerator Move(Transform Entity)
    {
        IsMoving = true ;
        StartPos = Entity.position ;
        T = 0 ;

        Endpos = new Vector3(StartPos.x + System.Math.Sign(InputPlayer.x), StartPos.y + System.Math.Sign(InputPlayer.y), StartPos.z);

        while(T < 1f)
        {
            T += Time.deltaTime * WalkSpeed ;
            Entity.position = Vector3.Lerp(StartPos, Endpos, T);
            yield return null ;
        }


        IsMoving = false ;
        yield return 0 ;
    }


    enum Direction
    {
        North,
        East,
        South,
        West
    }








}



