using AllosiusDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village.EncyclopaediaMenu
{
    public class LocationsList : MonoBehaviour
    {
        #region Fields

        public List<LocationStatus> statuses = new List<LocationStatus>();

        #endregion

        #region UnityInspector

        [SerializeField] private SamplableLibrary locationsLibrary;

        #endregion

        #region Events

        public event Action OnUpdate;

        #endregion

        #region Behaviour

        private void Awake()
        {
            InitStatuses();
        }

        public void InitStatuses()
        {
            for (int i = 0; i < locationsLibrary.library.Count; i++)
            {
                LocationData location = (LocationData)locationsLibrary.library[i];
                AddLocation(location);
            }
        }

        public IEnumerable<LocationStatus> GetStatuses()
        {
            return statuses;
        }

        public LocationStatus GetLocationStatus(LocationData location)
        {
            foreach (LocationStatus status in statuses)
            {
                if (status.GetLocation() == location)
                {
                    return status;
                }
            }

            return null;
        }

        public bool HasLocation(LocationData location)
        {
            return GetLocationStatus(location) != null;
        }

        public void AddLocation(LocationData location)
        {
            if (HasLocation(location)) return;
            LocationStatus newStatus = new LocationStatus(location);
            statuses.Add(newStatus);

            if (OnUpdate != null)
            {
                OnUpdate();
            }
            else
            {
                Debug.LogWarning("OnUpdate is null");
            }
        }

        public void CompleteLocationExploration(LocationData location)
        {
            LocationStatus status = GetLocationStatus(location);
            if (status != null && status.GetLocationAlreadyVisited() == false)
            {
                Debug.Log(status);
                status.SetLocationAlreadyVisited(true);

                if (OnUpdate != null)
                {
                    OnUpdate();
                }
                else
                {
                    Debug.LogWarning("OnUpdate is null");
                }
            }
        }

        #endregion
    }
}
