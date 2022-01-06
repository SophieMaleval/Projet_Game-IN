using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSync : MonoBehaviour
{
    public QuestSys questSys;
    public ActiveAsProg Aap;
    public GameObject title;
    public TextMeshProUGUI[] description;
    [SerializeField] int index;
    [SerializeField] int lvlID;
    public int currentStep;
    [SerializeField] string descName;
    //public List<string> steps = new List<string>();
    public string[] steps;    
    [HideInInspector]
    public int numberOfGoals;

    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        //AaP = GameObject.Find(.GetComponent<ActiveAsProg>();
    }
    public void SynchroniseText()
    {
        title = this.gameObject;       
        if (title.activeInHierarchy == true)
        {
            title.GetComponentInChildren<TextMeshProUGUI>().text = questSys.quest[index].questTitle;
            numberOfGoals = questSys.quest[index].questGoal.Length;           
            for (int i = 0; i < numberOfGoals; i++)
            {
                description[i].text = questSys.quest[index].questGoal[i];
               if (index <= questSys.lvlTracker) //&& questSys.etape > 1)
               {
                    description[i - 1].fontStyle = FontStyles.Strikethrough;
               }      
            }
        }
    }

    private void OnEnable()
    {
        SynchroniseText();
    }

    public void QuestChanged()
    {
        questSys.niveau = lvlID;
    }

    
    /*public void StrikeThrough()
    {               
               if (i >= etapeDeQuete)
               {
                    ecrits.fontStyle = FontStyles.Strikethrough;
              }
    }

    }*/
    
}
