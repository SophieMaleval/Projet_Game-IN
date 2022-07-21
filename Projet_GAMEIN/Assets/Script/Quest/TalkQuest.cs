using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifInventaire{
    Ajout,
    Retrait,
    None
}
public class TalkQuest : MonoBehaviour
{
    #region Fields

    private QuestSys questSys;
    private PlayerScript ScriptPlayer ;

    #endregion

    #region UnityInspector

    public ModifInventaire mode;
    public bool multipleObj;

    public Vector2 QuestEtape ;
    public InteractibleObject item ;

    #endregion

    #region Behaviour

    private void Awake()
    {
        questSys = GameManager.Instance.gameCanvasManager.questManager;
        ScriptPlayer = GameManager.Instance.player ;
    }

    public void TalkedTo()
    {
        if(QuestEtape.x == questSys.niveau && QuestEtape.y == questSys.etape)
        {
            if(mode == ModifInventaire.Retrait) 
            {
                ScriptPlayer.RemoveObject(item);
                if(GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(item, false);
            }

            if(mode == ModifInventaire.Ajout)
            {
                if(!ScriptPlayer.ItemChecker(item))
                {
                    Debug.Log("Add Item");
                    ScriptPlayer.AjoutInventaire(item);  
                    if(GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null) GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpItem(item, true);
                } else {
                    item.AddEntry();
                }     
            }

        }


        //questSys.Progression();        
        //Destroy(this.gameObject, 0.05f);
    }

    #endregion
}
