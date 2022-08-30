using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NpcData", menuName = "Village/NpcData")]
public class NpcData : ScriptableObject
{
    #region UnityInspector

    public string nameNpc;

    public int npcInENT;

    #endregion
}
