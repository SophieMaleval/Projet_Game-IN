using AllosiusDev;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcList : MonoBehaviour
{
    #region Fields

    private List<NpcStatus> statuses = new List<NpcStatus>();

    #endregion

    #region UnityInspector


    [SerializeField] private SamplableLibrary npcLibrary;

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
        for (int i = 0; i < npcLibrary.library.Count; i++)
        {
            NpcData npc = (NpcData)npcLibrary.library[i];
            AddNpc(npc);
        }

        if (OnUpdate != null)
        {
            OnUpdate();
        }
        else
        {
            Debug.LogWarning("OnUpdate is null");
        }
    }

    public List<NpcStatus> GetStatuses()
    {
        return statuses;
    }

    public NpcStatus GetNpcStatus(NpcData npc)
    {
        foreach (NpcStatus status in statuses)
        {
            if (status.GetNpc() == npc)
            {
                return status;
            }
        }

        return null;
    }

    public bool HasNpc(NpcData npc)
    {
        return GetNpcStatus(npc) != null;
    }

    public void AddNpc(NpcData npc)
    {
        if (HasNpc(npc)) return;
        NpcStatus newStatus = new NpcStatus(npc);
        statuses.Add(newStatus);
    }

    public void NpcTalked(NpcData npc)
    {
        NpcStatus status = GetNpcStatus(npc);
        if (status != null && status.GetNpcAlreadyTalked() == false)
        {
            Debug.Log(status);
            status.SetNpcAlreadyTalked(true);

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
