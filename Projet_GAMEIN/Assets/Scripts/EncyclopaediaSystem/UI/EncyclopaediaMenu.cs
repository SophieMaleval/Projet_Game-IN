using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaMenu : MonoBehaviour
    {
        #region Fields

        #endregion

        #region Properties


        #endregion

        #region UnityInspector

        [SerializeField] private Image titleDropdownBorder;
        [SerializeField] private Image panelBorder;

        [Space]

        [SerializeField] private ZoneButtonCtrl[] zonesButtons;

        [Space]

        [SerializeField] public LocationData currentLocation;

        [SerializeField] private Image placeImg;

        [SerializeField] private RectTransform[] npcSpawnPoints;
        [SerializeField] private EncyclopaediaNpcUI prefabEncyclopaediaNpcUi;

        [SerializeField] private Color unlockedPlaceColor;
        [SerializeField] private Color lockedPlaceColor;

        #endregion

        #region Behaviour

        private void Awake()
        {
            for (int i = 0; i < zonesButtons.Length; i++)
            {
                zonesButtons[i].SetSelectedButton(false);
            }
        }

        public void UpdateMenu()
        {
            Debug.Log("Redraw");

            if(GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive != null)
                currentLocation = GameManager.Instance.gameCanvasManager.inventory.QuestTrackingUi.currentQuestStepStatusActive.GetQuestStep().questLocationData;

            if(currentLocation == null)
            {
                return;
            }

            for (int i = 0; i < npcSpawnPoints.Length; i++)
            {
                foreach (Transform item in npcSpawnPoints[i])
                {
                    Destroy(item.gameObject);
                }
            }
            

            Color colorToApply = Color.black;
            if (GameManager.Instance.locationsList.GetLocationStatus(currentLocation).GetLocationAlreadyVisited())
            {
                colorToApply = unlockedPlaceColor;
            }
            else
            {
                colorToApply = lockedPlaceColor;
            }

            placeImg.sprite = currentLocation.spriteLocation;
            placeImg.color = colorToApply;

            for (int i = 0; i < currentLocation.spritesLocationNpc.Length; i++)
            {
                if(i < npcSpawnPoints.Length)
                {
                    EncyclopaediaNpcUI encyclopaediaNpc = Instantiate(prefabEncyclopaediaNpcUi, npcSpawnPoints[i].transform);
                    encyclopaediaNpc.NpcImg.sprite = currentLocation.spritesLocationNpc[i];
                    encyclopaediaNpc.NpcImg.color = colorToApply;
                }
            }

            for (int i = 0; i < zonesButtons.Length; i++)
            {
                if(zonesButtons[i].ZoneAssociated.zoneType == currentLocation.zone.zoneType)
                {
                    zonesButtons[i].SetSelectedButton(true);
                }
            }

            panelBorder.color = currentLocation.zone.zoneColor;
            titleDropdownBorder.color = currentLocation.zone.zoneColor;
        }

        #endregion
    }
}

