using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Stade{
    None,
    Preparation,
    Cuisson,
    Dressage
}
public class NoteObject : MonoBehaviour
{
    #region Fields

    private Transform noteHolder, detector;

    private Vector3 laPos;

    #endregion

    #region UnityInspector

    //public Stade stade;
    public bool canBePressed;
    public InputAction keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    #endregion

    #region Behaviour

    private void Awake()
    {
        //controls = new PlayerActionControls();

        keyToPress.performed += onPress;
        noteHolder = transform.parent.transform;
            
        //keyToPressed.canceled += WaitingForAttempt;
    }
    private void OnEnable() { keyToPress.Enable(); }
    private void OnDisable() { keyToPress.Disable(); }

    void onPress(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (canBePressed && RhythmManager.instance.gameExit == false)
            {
                gameObject.SetActive(false);
                
                if(Mathf.Abs(noteHolder.localPosition.y + transform.localPosition.y) > 0.25)
                {
                    //Debug.Log("Hit");
                    RhythmManager.instance.NormalHit();
                    //PlaySound();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if(Mathf.Abs(noteHolder.localPosition.y + transform.localPosition.y) > 0.05f)
                {
                    //Debug.Log("Good");
                    RhythmManager.instance.GoodHit();
                    //PlaySound();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    //Debug.Log("Perfect");
                    RhythmManager.instance.PerfectHit();
                    //PlaySound();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
            }

        }
    }
    private void FixedUpdate()
    {
        noteHolder = transform.parent.transform;
        laPos = noteHolder.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            detector = other.transform;
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                detector = null;
                canBePressed = false;
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
                RhythmManager.instance.NoteMissed();
            }
        }
    }

    /*public void PlaySound()
    {
        if (stade == Stade.Preparation) RhythmManager.instance.PrepSound();
        if (stade == Stade.Cuisson) RhythmManager.instance.FrySound();
        if (stade == Stade.Dressage) RhythmManager.instance.DressSound();
    }*/

    #endregion
}
