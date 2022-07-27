using AllosiusDev.Core;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllosiusDev.DialogSystem
{
    public class DialogTrigger : MonoBehaviour
    {
        #region Fields

        private bool alreadyUsed;

        private bool resetCam;

        #endregion

        #region UnityInspector

        [SerializeField] private NpcConversant npcConversant;

        [SerializeField] private bool singleUse;

        [SerializeField] private bool hasRequirements;
        [SerializeField] private GameRequirements gameRequirements;

        [Header("Animation Talk")]
        [SerializeField] private CinemachineVirtualCamera CineVCam;

        #endregion

        #region Behaviour

        void SwitchCineVCamTarget()
        {
            if (CineVCam == null)
            {
                return;
            }

            CineVCam.Follow = GameManager.Instance.player.transform;
            resetCam = false;
        }

        void Update()
        {
            if (alreadyUsed && !GameManager.Instance.player.InDiscussion && resetCam) SwitchCineVCamTarget();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasRequirements && !gameRequirements.ExecuteGameRequirements())
            {
                return;
            }

            if (singleUse && alreadyUsed)
            {
                return;
            }

            PlayerScript player = collision.GetComponent<PlayerScript>();
            if (player != null)
            {
                alreadyUsed = true;
                resetCam = true;
                if (CineVCam != null)
                {
                    CineVCam.Follow = npcConversant.transform;
                }
                npcConversant.StartDialog();
            }
        }

        #endregion
    }
}

