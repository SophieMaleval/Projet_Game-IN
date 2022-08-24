using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IconData", menuName = "Village/Icon Data")]
public class IconData : ScriptableObject
{
    #region UnityInspector

    [SerializeField] public Vector3 iconMoveUpOffset;
    [SerializeField] public Vector3 iconMoveDownOffset;
    [SerializeField] public float iconMoveDuration;
    [SerializeField] public float iconLifeDuration;
    [SerializeField] public Sprite iconSprite;

    #endregion
}
