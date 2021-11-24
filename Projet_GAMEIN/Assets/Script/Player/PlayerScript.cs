using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    [Header ("Input")]
    private PlayerInput playerInput;

    [Header ("Information")]
    public string PlayerName ;
    public int PlayerSexualGenre ;

    public GameObject input_VCue;
    public AudioSource selectedSound;

    public bool canInteract;
    public bool didFunction = false;


    //bool canInteract = false;

    //bool interactInput;

    //private void OnEnable() { controls.Enable(); }
    //private void OnDisable() { controls.Disable(); }
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        PlayerActionControls controls = new PlayerActionControls();
        controls.PlayerInLand.Enable();
        controls.PlayerInLand.Interact.performed += OnInteract;
    }
    // Start is called before the first frame update
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canInteract)
        {
            didFunction = true;
            Debug.Log("youhou! " + ctx.phase);
            selectedSound.Play();           
        }      
    }

    /*public bool DidInteract()
    {
        if (didFunction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/

    private void Update()
    {
        if (canInteract)
        {
            input_VCue.SetActive(true);
        }
        if(!canInteract)
        {
            input_VCue.SetActive(false);
        }
    }
}
