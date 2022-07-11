using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerNamesSelection", menuName = "Village/PlayerNamesSelection")]
public class PlayerNamesSelection : ScriptableObject
{
    #region UnityInspector

    public List<string> maleRandomNames = new List<string>();
    public List<string> femaleRandomNames = new List<string>();
    public List<string> mixedRandomNames = new List<string>();

    #endregion

    #region Behaviour

    public List<string> GetSumLists(List<string> list1, List<string> list2)
    {
        List<string> allRandomNames = list1;
        allRandomNames.AddRange(list2);
        return allRandomNames;
    }

    #endregion
}
