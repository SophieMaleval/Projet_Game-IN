using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public enum PlayerLookAffterMove
{
    Initial,
    Left,
    Up,
    Right
}

[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(InteractableElement))]
public class CameraTriggerVolume : MonoBehaviour
{
    #region Fields

    BoxCollider2D BoxCol ;
    Rigidbody2D Rb2D ;

    private bool PlayerAround = false ;

    private bool PlayerMoveOnScale = false ;

    #endregion

    #region UnityInspector

    [SerializeField] private CinemachineVirtualCamera Cam ;
    [SerializeField] private Vector3 BoxSize ;

    [Space]
    [SerializeField] private SceneEntManager ManagerENT ;
    [SerializeField] private int PartSceneValue ;
    
    [SerializeField] private bool ThisIsScale = false ;
    [SerializeField] private Vector2 NewScalePos ;
    [SerializeField] private PlayerLookAffterMove PlayerOrientation ;

    [SerializeField] private PlayerScript ScriptPlayer ;
    
    [SerializeField] private InteractableElement interactableElement;

    #endregion

    #region Behaviour

    private void Awake() 
    {
        BoxCol = GetComponent<BoxCollider2D>();
        Rb2D = GetComponent<Rigidbody2D>();

        BoxCol.isTrigger = true ;
        //BoxCol.size = BoxSize ;

        Rb2D.isKinematic = true ;

        if (ThisIsScale)
        {
            if(GameManager.Instance.player != null)
            {
                ScriptPlayer = GameManager.Instance.player;
            }
            else
            {
                Debug.LogWarning("Player Is null");
            }
        }
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
        PlayerScript player = other.GetComponent<PlayerScript>();

        if (player != null)
        {
            if(!ThisIsScale)
            {
                if(CameraSwitcher.ActiveCamera != Cam) CameraSwitcher.SwitchCamera(Cam) ;
                ManagerENT.ChangePartScene(PartSceneValue) ;  
            } else {
                PlayerAround = true ;          
                ScriptPlayer.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);          
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        PlayerScript player = other.GetComponent<PlayerScript>();

        if(player != null)
        {
            if(ThisIsScale)
            {
                PlayerAround = false ;
                ScriptPlayer.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
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
        ChangePlayerOrientation();
        /* FADE */ ScriptPlayer.LunchFadeOut();
        yield return new WaitForSeconds(0.5f);
        ScriptPlayer.GetComponent<PlayerMovement>().EndActivity();
        PlayerMoveOnScale = false ;
    }


    void ChangePlayerOrientation()
    {
        Vector2 NewAnimationPosValue ;
        NewAnimationPosValue = Vector2.zero ;

        if(PlayerOrientation == PlayerLookAffterMove.Initial) NewAnimationPosValue = new Vector2(0f, -1f);
        if(PlayerOrientation == PlayerLookAffterMove.Left) NewAnimationPosValue = new Vector2(-1f, 0f);
        if(PlayerOrientation == PlayerLookAffterMove.Up) NewAnimationPosValue = new Vector2(1f, 0f);
        if(PlayerOrientation == PlayerLookAffterMove.Right) NewAnimationPosValue = new Vector2(0f, 1f);
        
        ScriptPlayer.GetComponent<PlayerMovement>().GiveGoodAnimation(NewAnimationPosValue);
    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, BoxSize);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
    }

    #endregion
}