using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPNJ : MonoBehaviour
{
    #region Fields

    private SpriteRenderer SP ;

    #endregion

    #region UnityInspector

    public Sprite NormalSprite;
    public Sprite HighlightSprite;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Awake()
    {
        SP=GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        SP.sprite = HighlightSprite ;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        SP.sprite = NormalSprite ;
    }

    #endregion
}
