using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AllosiusDev.DialogSystem
{
    [RequireComponent(typeof(InteractableElement))]
    public class NpcConversant : MonoBehaviour
    {

        #region Fields


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

        #endregion

        #region Behaviour

        private void Awake()
        {
            InitAnimator();
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

        private void OnMouseOver()
        {
            if(GameManager.Instance.zoomActive == false)
            {
                canvasNpc.SetActive(true);
            }
        }

        private void OnMouseExit()
        {
            if (GameManager.Instance.zoomActive == false)
            {
                canvasNpc.SetActive(false);
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
