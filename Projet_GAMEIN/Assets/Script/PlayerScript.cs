using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    [Header ("Input")]
    private PlayerActionControls controls;

    [Header ("Information")]
    public string PlayerName ;
    public int PlayerSexualGenre ;

    

    bool interactInput;


    private void OnEnable() { controls.Enable(); }
    private void OnDisable() { controls.Disable(); }

    private void Awake()
    {
        controls = new PlayerActionControls();
        controls.PlayerInLand.Interact.performed += ctx => OnInteract();
    }
    // Start is called before the first frame update
    void OnInteract()
    {
        if (interactInput)
        {
            Debug.Log("youhou");
        }
        
    }

    private void Update()
    {
        
    }
}
