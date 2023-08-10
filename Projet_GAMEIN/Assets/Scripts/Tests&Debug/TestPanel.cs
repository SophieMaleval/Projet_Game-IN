using AllosiusDev.DialogSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    public Vector3 interactableSpritePosOffset;

    public float collisionRadius = 0.25f;

    private InteractableElement interactableElement;
    private float timeBeforeRelaunchDialogue = 0.5f;

    public GameObject active_poster;



    private bool canTalk = true;

    private bool PlayerAround = false;



    void ActivationObject()
    {
        if (PlayerAround == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (active_poster.activeSelf != true)
                {
                    active_poster.SetActive(true);
                }
                else
                {
                    active_poster.SetActive(false);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        ActivationObject();
    }

   // public void ActiveLocalCanvasObj()
   // {
   //     if (active_poster != null)
   //         active_poster.SetActive(true);
  // <summary>
  //  }
  // </summary>
  
    //public void DeactiveLocalCanvasObj()
   // {
   //     if (active_poster != null)
   //         active_poster.SetActive(false);
  //  }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       PlayerScript player = other.GetComponent<PlayerScript>();
            PlayerAround = true;
            GameManager.Instance.player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      PlayerScript player = other.GetComponent<PlayerScript>();

            PlayerAround = false;
            GameManager.Instance.player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
    }
}

