using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public InputAction keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    private void Awake()
    {
        //controls = new PlayerActionControls();
        keyToPress.performed += onPress;
        //keyToPressed.canceled += WaitingForAttempt;
    }
    private void OnEnable() { keyToPress.Enable(); }
    private void OnDisable() { keyToPress.Disable(); }

    void onPress(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                
                if(Mathf.Abs(transform.position.y) > 0.25)
                {
                    Debug.Log("Hit");
                    RhythmManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    RhythmManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    RhythmManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                RhythmManager.instance.NoteMissed();
            }
        }
    }
}
