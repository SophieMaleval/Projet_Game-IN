using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.EncyclopaediaMenu
{
    [CreateAssetMenu(fileName = "New LocationData", menuName = "Village/Locations/Location Data")]
    public class LocationData : ScriptableObject
    {
        #region UnityInspector

        public string locationName;

        public Sprite spriteLocation;

        public LocationNpc[] locationNpc;

        [SerializeField] public LocationZone zone;

        #endregion

        #region Structs

        [Serializable]
        public struct LocationNpc
        {
            public Sprite spriteLocationNpc;
            public NpcData dataLocationNpc;
        }

        #endregion
    }




}
