using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.EncyclopaediaMenu
{
    [Serializable]
    public class LocationStatus
    {
        #region Fields

        private LocationData location;

        private bool locationAlreadyVisited;

        #endregion

        #region Properties

        public LocationStatus(LocationData _location)
        {
            this.location = _location;
        }

        #endregion

        #region UnityInspector

        [SerializeField] private string locationName;

        #endregion

        #region Functions

        public string GetLocationName()
        {
            return locationName;
        }

        public LocationData GetLocation()
        {
            return location;
        }

        public bool GetLocationAlreadyVisited()
        {
            return locationAlreadyVisited;
        }


        public string SetLocationName(string name)
        {
            locationName = name;
            return GetLocationName();
        }

        public void SetLocationAlreadyVisited(bool value)
        {
            locationAlreadyVisited = value;
        }

        #endregion
    }
}
