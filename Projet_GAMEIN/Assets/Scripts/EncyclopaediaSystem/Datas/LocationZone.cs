using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LocationZone", menuName = "Village/Locations/LocationZone Data")]
public class LocationZone : ScriptableObject
{
    public enum ZoneType
    {
        Ville,
        ZoneIndustrielle,
        Foret,
        Plage,
    }

    public ZoneType zoneType;

    public Color zoneColor;

    public string zoneName;
}
