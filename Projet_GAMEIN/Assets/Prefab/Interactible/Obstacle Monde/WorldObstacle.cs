using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldObstacle : MonoBehaviour
{
    private SpriteRenderer SpriteRend ;

    [SerializeField] private Sprite DisableSprite ;
    [SerializeField] private Sprite EnableSprite ;

    [SerializeField] private Collider2D PassageDisableColl ;
    [SerializeField] private Collider2D PassageEnableColl ;

    private void Awake() {
        SpriteRend = GetComponent<SpriteRenderer>();
    }

    void SetPassage(bool State)
    {
        if(!State)
        {
            SpriteRend.sprite = DisableSprite ;
            PassageDisableColl.enabled = true ;
            PassageEnableColl.enabled = false ;
        } else {
            SpriteRend.sprite = EnableSprite ;
            PassageDisableColl.enabled = false ;
            PassageEnableColl.enabled = true ;
        }
    }
}
