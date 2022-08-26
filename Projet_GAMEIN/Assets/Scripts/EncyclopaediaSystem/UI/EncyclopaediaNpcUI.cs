using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaNpcUI : MonoBehaviour
    {
        #region Properties

        public Image NpcImg => npcImg;

        #endregion

        #region UnityInspector

        [SerializeField] private Image npcImg;

        #endregion
    }
}

