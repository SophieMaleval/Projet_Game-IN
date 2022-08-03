using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PopUpData", menuName = "Village/PopUpData")]
public class PopUpData : ScriptableObject
{
    #region UnityInspector

    public string titleTranslationKey;
    public string descriptionTranslationKey;

    public Sprite spriteToDisplay;

    #endregion
}
