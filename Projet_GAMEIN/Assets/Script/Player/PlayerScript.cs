using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI ;
using DG.Tweening;
using AllosiusDev.Audio;

public class PlayerScript : MonoBehaviour
{
    #region Fields

    [Header ("Inputs")]
    private PlayerActionControls PlayerActionControllers ;

    #endregion

    #region Properties

    public string PlayerName { get; set; }

    //[Header ("Canvas Location")]
    public GameObject CanvasIndestrucitble { get; set; }
    public GameObject DialogueUIIndestructible { get; set; }
    public GameObject InventoryUIIndestructible { get; set; }
    public GameObject PannelENTUIIndestructible { get; set; }
    public GameObject PannelAnnonceUIIndestructible { get; set; }
    public GameObject QCMPanelUIIndestructible { get; set; }

    //public string PreviousSceneName { get; set; }
    public SceneData PreviousSceneName { get; set; }

    #endregion

    #region UnityInspector

    [Header ("Information")]
    public int PlayerSexualGenre ;

    public GameObject InterractInputSprite;
    //public AudioSource selectedSound;


    public bool CanCollectObject = true ;
    public bool PlayerAsInterract;
    public bool InDiscussion = false ;

    //public InteractibleObject[] Inventaire = new InteractibleObject[] {} ;
    public List<InteractibleObject> Inventaire = new List<InteractibleObject>() ;
    public TLManager TimeLineManager ;

    public Vector2 MainSceneLoadPos ;
    [HideInInspector] public bool InAnimationFade = false ;
    [HideInInspector] public bool FadeMake = false ;
    [HideInInspector] public bool AnimationBeMake = false ;

    //public GameObject globalLightPlayer;

    public PlayerDialogue playerBackpack;

    [HideInInspector] public Image FadeAnimation ;

    [Header("Sounds")]
    [SerializeField] private AudioData selectedSound;

    #endregion

    #region Behaviour

    private void OnEnable() { PlayerActionControllers.Enable(); }
    private void OnDisable() { PlayerActionControllers.Disable(); } 

    private void Awake()
    {
        PlayerActionControllers = new PlayerActionControls();
        PlayerActionControllers.PlayerInLand.Interact.performed += OnInteract;
        PlayerActionControllers.PlayerInLand.Inventory.performed += OnInventory;

        if(GameManager.Instance.player == null)
        {
            GameManager.Instance.player = this;
        }
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
        if(InventoryUIIndestructible != null && InventoryUIIndestructible.activeInHierarchy)
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
       /* for (int I = 0; I < Inventaire.Count; I++)
        {
            if(Inventaire[I] == null)
            {
                Inventaire[I] = ObjetAjouter ;
                selectedSound.Play();
                break ;
            }
        }*/
        Inventaire.Add(ObjetAjouter) ;
        AudioController.Instance.PlayAudio(selectedSound);
        //AskInventairePlein();
    }

    public bool ItemChecker(InteractibleObject ObjectSearch)
    {
        bool returned = false;
        for (int I = 0; I < Inventaire.Count; I++)
        {
            if (Inventaire[I] != null)
            {
                if((PlayerPrefs.GetInt("Langue") == 0 && Inventaire[I].NameFR == ObjectSearch.NameFR) || (PlayerPrefs.GetInt("Langue") == 1 && Inventaire[I].NameEN == ObjectSearch.NameEN))
                {
                    returned = true;
                }
            }
        }
        Debug.Log("ItemChecker : " + ObjectSearch.NameFR + " " + returned);
        return returned;
    }

    public void RemoveObject(InteractibleObject ObjectRemove)
    {
        Inventaire.Remove(ObjectRemove) ;
        /*for (int I = 0; I < Inventaire.Count; I++)
        {
            if (Inventaire[I] != null)
            {
                if(Inventaire[I] == ObjectRemove)
                {
                    Inventaire.Remove(ObjectRemove) ;
                }
            }
        }   */
    }

    void AskInventairePlein() // Pour Inventaire limité
    {
        if(Inventaire[Inventaire.Count - 1] == null)
            CanCollectObject = true ;
        else
            CanCollectObject = false ;
    }

    [ContextMenu("LunchAnimationFadeIn")]
    public void LunchAnimationFadeIn()
    {
        StartCoroutine(FadeInIEnum());
    }

    [ContextMenu("LaunchFade")]
    public void LaunchFade(float fadeDuration = 0.5f, float fadeOutSwitchDuration = 1f)
    {
        StartCoroutine(Fade(fadeDuration, fadeOutSwitchDuration));
    }
 
    IEnumerator FadeInIEnum()
    {
        Debug.Log("FadeInIEnum");

        InAnimationFade = true ;
        FadeAnimation.raycastTarget = true ;
        FadeAnimation.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(1f);
        FadeMake = true ;        
    }

    IEnumerator Fade(float fadeDuration, float fadeOutSwitchDuration)
    {
        FadeAnimation.raycastTarget = true;
        FadeAnimation.DOFade(1f, fadeDuration);
        yield return new WaitForSeconds(fadeOutSwitchDuration);
        FadeAnimation.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(0.25f);
        FadeAnimation.raycastTarget = false;
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

    #endregion
}
