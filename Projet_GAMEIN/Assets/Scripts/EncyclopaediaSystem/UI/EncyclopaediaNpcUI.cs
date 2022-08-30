using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Village.EncyclopaediaMenu
{
    public class EncyclopaediaNpcUI : MonoBehaviour
    {
        #region Properties

        public Image NpcImg => npcImg;

        public GameObject NpcNameObj => npcNameObj;
        public TextMeshProUGUI NpcNameLabel => npcNameLabel;

        #endregion

        #region UnityInspector

        [SerializeField] private Image npcImg;

        [SerializeField] private GameObject npcNameObj;
        [SerializeField] private TextMeshProUGUI npcNameLabel;

        #endregion
    }
}

