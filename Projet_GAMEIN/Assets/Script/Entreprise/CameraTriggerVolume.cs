﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Cam ;
    [SerializeField] private Vector3 BoxSize ;

    BoxCollider2D BoxCol ;
    Rigidbody2D Rb2D ;

    [Space]
    [SerializeField] private SceneEntManager ManagerENT ;
    [SerializeField] private int PartSceneValue ;
    
    [SerializeField] private bool ThisIsScale = false ;
    [SerializeField] private Vector2 NewScalePos ;
    private bool PlayerAround = false ;


    [SerializeField] private PlayerScript ScriptPlayer ;
    private bool PlayerMoveOnScale = false ;


    private void Awake() 
    {
        BoxCol = GetComponent<BoxCollider2D>();
        Rb2D = GetComponent<Rigidbody2D>();
        BoxCol.isTrigger = true ;
        BoxCol.size = BoxSize ;

        Rb2D.isKinematic = true ;  

        if(ThisIsScale && GameObject.Find("Player") != null) ScriptPlayer = GameObject.Find("Player").GetComponent<PlayerScript>() ;           
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }

    private void Update() {
        if(ThisIsScale && PlayerAround && !PlayerMoveOnScale)
        {
            if(ScriptPlayer.PlayerAsInterract)
            {
                ScriptPlayer.PlayerAsInterract = false ;
                StartCoroutine(MoveOnScale());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(!ThisIsScale)
            {
                if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
                ManagerENT.ChangePartScene(PartSceneValue) ;  
            } else {
                PlayerAround = true ;          
                ScriptPlayer.SwitchInputSprite();          
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(ThisIsScale)
            {
                PlayerAround = false ;
                ScriptPlayer.SwitchInputSprite();
            }
        }
    }

    public void SetFirstCamera()
    {
        if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
    }


    IEnumerator MoveOnScale()
    {
        PlayerMoveOnScale = true ;
        ScriptPlayer.GetComponent<PlayerMovement>().StartActivity();
        /* FADE */ ScriptPlayer.LunchAnimationFadeIn();
        yield return new WaitForSeconds(0.5f);
        ScriptPlayer.transform.position = NewScalePos ;
        if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
        ManagerENT.ChangePartScene(PartSceneValue) ;  
        /* FADE */ ScriptPlayer.LunchFadeOut();
        yield return new WaitForSeconds(0.5f);
        ScriptPlayer.GetComponent<PlayerMovement>().EndActivity();
        PlayerMoveOnScale = false ;
    }
}