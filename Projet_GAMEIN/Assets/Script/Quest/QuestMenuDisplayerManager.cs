using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenuDisplayerManager : MonoBehaviour
{
    [SerializeField] private List<QuestInMenu> QuestTitle = new List<QuestInMenu>() ;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuestManager(int QuestTarget)
    {
      /*  TargetQuestManager(QuestTarget);
        ArrowManager(QuestTarget);*/

        for (int TQD = 0; TQD < QuestTitle.Count; TQD++)
        {
            if(TQD != QuestTarget)
            {
                QuestTitle[TQD].CloseDropdown();
                QuestTitle[TQD].ClosedArrow();
            } else {
                if(!QuestTitle[TQD].isOpen)
                {
                    QuestTitle[TQD].Dropdown();
                    QuestTitle[TQD].OpenArrow();
                } else {
                    QuestTitle[TQD].CloseDropdown();
                    QuestTitle[TQD].ClosedArrow();
                } 
            }
        }
    }


    void TargetQuestManager(int QuestDisplay)
    {
        for (int TQD = 0; TQD < QuestTitle.Count; TQD++)
        {
            if(TQD != QuestDisplay)
            {
                QuestTitle[TQD].CloseDropdown();
            } else {
                if(!QuestTitle[TQD].isOpen) QuestTitle[TQD].Dropdown();
                else QuestTitle[TQD].CloseDropdown();
            }
        }
    }

    void ArrowManager(int MenuDisplay)
    {
        for (int Q = 0; Q < QuestTitle.Count; Q++)
        {
            if(Q != MenuDisplay)
            {
                QuestTitle[Q].ClosedArrow();
            } else {
                if(!QuestTitle[Q].isOpen) QuestTitle[Q].OpenArrow();
                else QuestTitle[Q].ClosedArrow();
            }
        }
    }
}
