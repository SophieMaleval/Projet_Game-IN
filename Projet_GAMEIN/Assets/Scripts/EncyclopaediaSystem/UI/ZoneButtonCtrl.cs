using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village.EncyclopaediaMenu
{
    public class ZoneButtonCtrl : MonoBehaviour
    {
        #region Properties

        public LocationZone ZoneAssociated => zoneAssociated;

        #endregion

        #region UnityInspector

        [SerializeField] private GameObject selectionCursor;
        [SerializeField] private Image selectionCursorBorder;
        [SerializeField] private Image icon;

        [SerializeField] private Color selectedIconColor;
        [SerializeField] private Color unselectedIconColor;

        [SerializeField] private LocationZone zoneAssociated;

        #endregion

        #region Behaviour

        private void Awake()
        {
            selectionCursorBorder.color = zoneAssociated.zoneColor;
        }

        public void SetSelectedButton(bool value)
        {
            selectionCursor.SetActive(value);

            if(value)
            {
                icon.color = selectedIconColor;
            }
            else
            {
                icon.color = unselectedIconColor;
            }
            
        }

        #endregion
    }
}
