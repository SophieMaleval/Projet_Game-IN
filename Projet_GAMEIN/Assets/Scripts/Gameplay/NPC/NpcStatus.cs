using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NpcStatus
{
    #region Fields

    private NpcData npc;

    private bool npcAlreadyTalked;

    #endregion

    #region Properties

    public NpcStatus(NpcData _npc)
    {
        this.npc = _npc;
    }

    #endregion

    #region Functions

    public NpcData GetNpc()
    {
        return npc;
    }

    public bool GetNpcAlreadyTalked()
    {
        return npcAlreadyTalked;
    }


    public void SetNpcAlreadyTalked(bool value)
    {
        npcAlreadyTalked = value;
    }
    #endregion
}
