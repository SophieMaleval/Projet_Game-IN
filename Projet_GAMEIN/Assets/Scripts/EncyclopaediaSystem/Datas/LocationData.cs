using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.EncyclopaediaMenu
{
    [CreateAssetMenu(fileName = "New LocationData", menuName = "Village/Location Data")]
    public class LocationData : ScriptableObject
    {
        #region UnityInspector

        public Sprite spriteLocation;

        public Sprite[] spritesLocationNpc;

        #endregion
    }
}
