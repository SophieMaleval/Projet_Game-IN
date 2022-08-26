using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Village.EncyclopaediaMenu;

[CreateAssetMenu(fileName = "New SceneData", menuName = "Village/SceneData")]
public class SceneData : ScriptableObject
{
    #region UnityInspector

    public Scenes sceneToLoad;

    public bool isGameScene = true;

    public bool sceneOutside;

    [Tooltip("Data of the place associated to the scene (if we are in a specific building/place)")]
    public LocationData sceneLocation;


    #endregion
}
