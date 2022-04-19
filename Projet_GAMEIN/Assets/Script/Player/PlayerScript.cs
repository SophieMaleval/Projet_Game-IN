using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI ;
using DG.Tweening;


public class PlayerScript : MonoBehaviour
{
    [Header ("Inputs")]
    private PlayerActionControls PlayerActionControllers ;

    [Header ("Information")]
    public string PlayerName ;
    public int PlayerSexualGenre ;

    public GameObject InterractInputSprite;
    public AudioSource selectedSound;


    public bool CanCollectObject = true ;
    public bool PlayerAsInterract;
    public bool InDiscussion = false ;

    public InteractibleObject[] Inventaire = new InteractibleObject[] {} ;
    public TLManager TimeLineManager ;

    public Vector2 MainSceneLoadPos ;
    public string PreviousSceneName ;
    [HideInInspector] public bool InAnimationFade = false ;
    [HideInInspector] public bool FadeMake = false ;
    [HideInInspector] public bool AnimationBeMake = false ;

    [Header ("Canvas Location")]
    public GameObject CanvasIndestrucitble ;
    public GameObject DialogueUIIndestructible ;
    public GameObject InventoryUIIndestructible ;
    public GameObject PannelENTUIIndestructible ;
    public GameObject PannelAnnonceUIIndestructible ;
    public GameObject QCMPanelUIIndestructible ;
    [HideInInspector] public Image FadeAnimation ;


    private void OnEnable() { PlayerActionControllers.Enable(); }
    private void OnDisable() { PlayerActionControllers.Disable(); } 

    private void Awake()
    {
        PlayerActionControllers = new PlayerActionControls();
        PlayerActionControllers.PlayerInLand.Interact.performed += OnInteract;
        PlayerActionControllers.PlayerInLand.Inventory.performed += OnInventory;
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if(GetComponent<PlayerMovement>().enabled == true) // Bloque si PlayerMovement disable
        {
            if(ctx.performed) 
            {
                PlayerAsInterract = true ;
                StopCoroutine(DisablePlayerInterract());
                StartCoroutine(DisablePlayerInterract());          
            }

        }      
    }
  
    IEnumerator DisablePlayerInterract()
    {
        yield return new WaitForSeconds(0.05f);
        PlayerAsInterract = false ;
    }

    public void OnInventory(InputAction.CallbackContext ctx)
    {
        if(GetComponent<PlayerMovement>().enabled == true) // Bloque si PlayerMovement disable
        {
            if(ctx.performed) 
            {
                InventoryInteract();       
            }
        }      
    }

    void InventoryInteract()
    {
        if(InventoryUIIndestructible != null)
        {
            InventoryUIIndestructible.GetComponent<InventoryScript>().SwitchToggleInventoryDisplay();
        }
    }

    private void Update()
    {

    }

    public void SwitchInputSprite()
    {
        InterractInputSprite.SetActive(!InterractInputSprite.activeSelf);
    }

    public void InputSpritePos(bool StatePositif)
    {
        if(StatePositif) // La touche se positionne à droite du joueur
            InterractInputSprite.transform.localPosition = new Vector3(0.25f, InterractInputSprite.transform.localPosition.y, InterractInputSprite.transform.localPosition.z) ;
        else // Latouche se positionne à gauche du joueur
            InterractInputSprite.transform.localPosition = new Vector3(-0.25f, InterractInputSprite.transform.localPosition.y, InterractInputSprite.transform.localPosition.z) ;
    }


    public void AjoutInventaire(InteractibleObject ObjetAjouter)
    {
        for (int I = 0; I < Inventaire.Length; I++)
        {
            if(Inventaire[I] == null)
            {
                Inventaire[I] = ObjetAjouter ;
                selectedSound.Play();
                break ;
            }
        }

        AskInventairePlein();
    }

    public bool ItemChecker(InteractibleObject ObjectSearch)
    {
        bool returned = false;
        for (int I = 0; I < Inventaire.Length; I++)
        {
            if (Inventaire[I] != null)
            {
                if(Inventaire[I].Name == ObjectSearch.Name)
                {
                    returned = true;
                }
            }
        }
        return returned;
    }

    public void RemoveObject(InteractibleObject ObjectRemove)
    {
        for (int I = 0; I < Inventaire.Length; I++)
        {
            if (Inventaire[I] != null)
            {
                if(Inventaire[I] == ObjectRemove)
                {
                    Inventaire[I] = null ;
                }
            }
        }   
    }

    void AskInventairePlein()
    {
        if(Inventaire[Inventaire.Length-1] == null)
            CanCollectObject = true ;
        else
            CanCollectObject = false ;
    }

    public void LunchAnimationFadeIn()
    {
        StartCoroutine(FadeInIEnum());
    }
 
    IEnumerator FadeInIEnum()
    {
        InAnimationFade = true ;
        FadeAnimation.raycastTarget = true ;
        FadeAnimation.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(1f);
        FadeMake = true ;        
    }

    public void LunchFadeOut()
    {
        StartCoroutine(FadeOutIEnum());

     /*   AnimationBeMake = true ;        
        FadeMake = false ;
        FadeAnimation.DOFade(0f, 0.5f);
        InAnimationFade = false ;
        FadeAnimation.raycastTarget = false ;   */
    }


    IEnumerator FadeOutIEnum()
    {
        AnimationBeMake = true ;        
        FadeMake = false ;
        FadeAnimation.DOFade(0f, 0.5f);
        yield return new WaitForSeconds(0.25f);
        InAnimationFade = false ;
        FadeAnimation.raycastTarget = false ;    
    }


}
