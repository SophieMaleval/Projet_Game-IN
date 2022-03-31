using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private Sprite defaultImage;

    [SerializeField]
    private Sprite pressedImage;


    public InputAction arrow;

    private void Awake()
    {
        //controls = new PlayerActionControls();
        arrow.performed += onAttempt;
        arrow.canceled += WaitingForAttempt;
    }
    private void OnEnable() { arrow.Enable(); }
    private void OnDisable() { arrow.Disable();}

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultImage;
        //controls.PlayerInLand.Disable();
    }
    
    //Appeler en InputAction.Callback context les inputs, puis les assigner à chaque bouton


    // Update is called once per frame
    void onAttempt(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            spriteRenderer.sprite = pressedImage;
            //Debug.Log("button pressed");
        }
    }

    void WaitingForAttempt(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            spriteRenderer.sprite = defaultImage;
            //Debug.Log("waiting for an attempt");
        }
    }
}
