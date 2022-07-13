using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneData", menuName = "Village/SceneData")]
public class SceneData : ScriptableObject
{
    #region UnityInspector

    public Scenes sceneToLoad;

    public bool sceneOutside;

    #endregion
}
