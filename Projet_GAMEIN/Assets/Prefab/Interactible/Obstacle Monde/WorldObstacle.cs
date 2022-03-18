using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class WorldObstacle : MonoBehaviour
{
    private SpriteRenderer SpriteRend ;

    [SerializeField] private GameObject ObstacleDisableGroup ;
    [SerializeField] private GameObject ObstacleEnableGroup ;

    [SerializeField] private Sprite DisableSprite ;
    [SerializeField] private Sprite EnableSprite ;

    [SerializeField] private Collider2D PassageDisableColl ;
    [SerializeField] private Collider2D PassageEnableColl ;

    private void Awake() {
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    public void SetPassage(bool State)
    {
        if(!State)
        {
            if(ObstacleDisableGroup != null || ObstacleEnableGroup != null)
            {
                if(ObstacleDisableGroup != null) ObstacleDisableGroup.SetActive(true);
                if(ObstacleEnableGroup != null) ObstacleEnableGroup.SetActive(false);
            } else {
                SpriteRend.sprite = DisableSprite ;                
            }
            if(PassageDisableColl != null) PassageDisableColl.enabled = true ;
            if(PassageEnableColl != null) PassageEnableColl.enabled = false ;
        } else {
            if(ObstacleDisableGroup != null || ObstacleEnableGroup != null)
            {
                if(ObstacleDisableGroup != null) ObstacleDisableGroup.SetActive(false);
                if(ObstacleEnableGroup != null) ObstacleEnableGroup.SetActive(true);
            } else {
                SpriteRend.sprite = EnableSprite ;                
            }

            if(PassageDisableColl != null) PassageDisableColl.enabled = false ;
            if(PassageEnableColl != null) PassageEnableColl.enabled = true ;
        }
    }
}
