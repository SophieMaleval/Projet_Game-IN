﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AllosiusDev.DialogSystem
{
    [RequireComponent(typeof(InteractableElement))]
    public class NpcConversant : MonoBehaviour
    {

        #region Fields

        private bool mouseOver;

        private bool canTalk = true;

        private bool PlayerAround = false;

        #endregion

        #region Properties

        public string NamePnj => namePNJ;

        #endregion

        #region UnityInspector

        [Header("PNJ Information")]

        [SerializeField] private string namePNJ;
        [SerializeField] private int pnjInENT;

        [SerializeField] private DialogueGraph npcDialogue;

        [SerializeField] private float timeBeforeRelaunchDialogue = 0.5f;

        [SerializeField] private InteractableElement interactableElement;

        [SerializeField] private GameObject canvasNpc;

        [SerializeField] private LayerMask layerMouseSelectables;

        #endregion

        #region Behaviour

        private void Awake()
        {
            InitAnimator();

            if (canvasNpc != null)
            {
                canvasNpc.SetActive(false);
            }

            if (GameCore.Instance != null)
            {
                GameCore.Instance.onDezoomActive += ActiveLocalCanvasObj;
                GameCore.Instance.onDezoomDeactive += DeactiveLocalCanvasObj;
            }
        }

        public void ActiveLocalCanvasObj()
        {
            if(canvasNpc != null)
                canvasNpc.SetActive(true);
        }

        public void DeactiveLocalCanvasObj()
        {
            if(canvasNpc != null)
                canvasNpc.SetActive(false);
        }

        void OnEnable()
        {
            Debug.Log("NPC Conversant is active");

            InitAnimator();
        }

        private void Update()
        {
            if (PlayerAround)
            {
                if (GameManager.Instance.player.PlayerAsInterract && !GameManager.Instance.player.InDiscussion)
                {
                    Debug.Log("Launch Discussion " + npcDialogue.displayName);
                    GameManager.Instance.player.debug.color = Color.blue;
                    StartDialog();
                }
            }

            MouseOverDetection();
        }

        public void SetCanTalk(bool value)
        {
            canTalk = value;
        }

        public IEnumerator ResetCanTalk()
        {
            canTalk = false;

            yield return new WaitForSeconds(timeBeforeRelaunchDialogue);

            canTalk = true;
        }

        public void InitAnimator()
        {
            GetComponent<Animator>().SetInteger("PNJ Need", pnjInENT);
        }

        public void PNJTalkAnimation(bool IsTalking)
        {
            GetComponent<Animator>().SetBool("Talk", IsTalking);
        }

        public void StartDialog()
        {
            if (npcDialogue == null || canTalk == false)
            {
                return;
            }

            Debug.Log("Player Start Dialogue");
            GameManager.Instance.player.GetComponent<PlayerConversant>().StartDialog(this, npcDialogue);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                PlayerAround = true;
                GameManager.Instance.player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                PlayerAround = false;
                GameManager.Instance.player.SwitchInputSprite(transform, interactableElement.interactableSpritePosOffset);
            }
        }

        private void MouseOverDetection()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, layerMouseSelectables);
            if (hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);

                NpcConversant npcConversant = hit.collider.gameObject.GetComponent<NpcConversant>();
                if (npcConversant != null && npcConversant == this)
                {

                    SetCanvasNpcActive(true);
                }
                else
                {
                    SetCanvasNpcActive(false);
                }
            }
            else
            {
                SetCanvasNpcActive(false);
            }
        }

        private void SetCanvasNpcActive(bool value)
        {
            if(GameManager.Instance.zoomActive == false)
            {
                if (mouseOver == !value)
                {
                    if (value)
                    {
                        Debug.Log("Mouse Over");
                    }
                    else
                    {
                        Debug.Log("Mouse Exit");
                    }
                    canvasNpc.SetActive(value);
                    mouseOver = value;
                }
            }
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position + interactableElement.interactableSpritePosOffset, interactableElement.collisionRadius);
        }

        #endregion
    }
}
