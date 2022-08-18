using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LeverGroup
{
    LeverGroup1,
    LeverGroup2,
    LeverGroup3,
    LeverGroup4,
    LeverGroup5
}

[RequireComponent(typeof(InteractableElement))]
public class LeverController : MonoBehaviour
{
    [Header ("Lever Gestion")]
    [SerializeField] private List<SpriteRenderer> LeverRender = new List<SpriteRenderer>();
    //private bool LeverIsActivate = false ;
    [SerializeField] private Sprite LeverDisable ;
    [SerializeField] private Sprite LeverDisableHighlight ;
    [SerializeField] private Sprite LeverEnable ;
    [SerializeField] private Sprite LeverEnableHighlight ;
    [Space]
    [SerializeField] private List<WorldObstacle> ObstacleInitiallyDisable = new List<WorldObstacle>() ;
    [SerializeField] private List<WorldObstacle> ObstacleInitiallyEnable = new List<WorldObstacle>() ;

    [SerializeField] private LeverGroup GroupOfLever;
    private PlayerScript Player ;
    private bool PlayerAround = false;

    [SerializeField] private InteractableElement interactableElement;

    private void Awake() {
        if(GameObject.Find("Player") != null)   // Récupère le player au lancement de la scène
        {    Player = GameObject.Find("Player").GetComponent<PlayerScript>() ; }

        if(GetComponent<SpriteRenderer>() != null && LeverRender.Count == 0) LeverRender.Add(GetComponent<SpriteRenderer>());
    }

    void Start() 
    {
        ActivationLever(false);
    }

    void Update() 
    {
        if(PlayerAround)
        {
            if(Player.PlayerAsInterract)
            {
                Player.PlayerAsInterract = false ;
                ActivationLever(true);
            }
        }
    }



    void ActivationLever(bool IsAnInterract)
    {
        if(IsAnInterract)
        {
            if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 0) PlayerPrefs.SetInt(nameof(GroupOfLever), 1);
            else PlayerPrefs.SetInt(nameof(GroupOfLever), 0);    
        }


        if(ObstacleInitiallyDisable.Count != 0)
        {
            for (int WO = 0; WO < ObstacleInitiallyDisable.Count; WO++)
            {
                if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 0) ObstacleInitiallyDisable[WO].SetPassage(true);
                else ObstacleInitiallyDisable[WO].SetPassage(false);
            }            
        }

        if(ObstacleInitiallyEnable.Count != 0)
        {
            for (int WO = 0; WO < ObstacleInitiallyEnable.Count; WO++)
            {
                if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 0) ObstacleInitiallyEnable[WO].SetPassage(false);
                else ObstacleInitiallyEnable[WO].SetPassage(true);
            }            
        }


        for (int Sp = 0; Sp < LeverRender.Count; Sp++)
        {
            if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 0) LeverRender[Sp].sprite = LeverDisable ;
            else LeverRender[Sp].sprite = LeverEnable ;
        }




        if(PlayerAround)    ShowHighlightLever(true);      


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            PlayerAround = true ;
            ShowHighlightLever(true);
            Player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }      
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == ("Player"))
        {
            PlayerAround = false ;
            ShowHighlightLever(false);
            Player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
        }
    }

    void ShowHighlightLever(bool StateHighlight)
    {
        for (int Sp = 0; Sp < LeverRender.Count; Sp++)
        {
            if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 0)
            {
                if(!StateHighlight) LeverRender[Sp].sprite = LeverDisable ;
                else LeverRender[Sp].sprite = LeverDisableHighlight ;
            }

            if(PlayerPrefs.GetInt(nameof(GroupOfLever)) == 1)
            {
                if(!StateHighlight) LeverRender[Sp].sprite = LeverEnable ;
                else LeverRender[Sp].sprite = LeverEnableHighlight ;
            }
        }
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
    }

    #endregion
}
