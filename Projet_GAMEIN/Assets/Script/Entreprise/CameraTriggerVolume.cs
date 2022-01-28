using System.Collections;
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

    private void Awake() 
    {
        BoxCol = GetComponent<BoxCollider2D>();
        Rb2D = GetComponent<Rigidbody2D>();
        BoxCol.isTrigger = true ;
        BoxCol.size = BoxSize ;

        Rb2D.isKinematic = true ;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, BoxSize);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if(other.gameObject.CompareTag("Player"))
        if(other.gameObject.tag == "Player")
        {
            if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
            
            ManagerENT.ChangePartScene(PartSceneValue) ;
        }
    }

    public void SetFirstCamera()
    {
        if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
    }
}
