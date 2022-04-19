using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSync : MonoBehaviour
{
    public QuestSys questSys;
    //public Checker checker;
    //public ActiveAsProg Aap;
    public GameObject title;
    public GameObject[] descObj;
    public TextMeshProUGUI[] description;
    [SerializeField] int index;
    [SerializeField] int lvlID;
    //public int currentStep;
    [SerializeField] string descName;
    //public List<string> steps = new List<string>();
   // public string[] steps;    
    [HideInInspector]
    public int numberOfGoals;
    public QuestInMenu buttons;

    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        descObj = GameObject.FindGameObjectsWithTag(descName);
        title = this.gameObject;
        //AaP = GameObject.Find(.GetComponent<ActiveAsProg>();
    }
    public void SynchroniseText()
    {
        title = this.gameObject;
        if (title.activeInHierarchy == true)
        {
            title.GetComponentInChildren<TextMeshProUGUI>().text = questSys.quest[index].questTitle;
            numberOfGoals = questSys.quest[index].questGoal.Count;
            for (int i = 0; i < numberOfGoals; i++)
            {
                description[i].text = questSys.quest[index].questGoal[i];

                if (index < questSys.lvlTracker + 1 && index != 0)
                {
                    description[i].fontStyle = FontStyles.Strikethrough;
                    title.GetComponent<Button>().interactable = false;
                }
                else if (index == questSys.niveau && i < questSys.etape)
                {
                    description[i].fontStyle = FontStyles.Strikethrough;
                }
                else
                {
                    description[i].fontStyle = FontStyles.Normal;
                }
            }
        }
    }

    public void HideProgress()
    {
        buttons = this.gameObject.GetComponent<QuestInMenu>();
        if (buttons.isOpen)
        {
            //buttons.OpenArrow();
            if (title.activeInHierarchy == true)
            {
                numberOfGoals = questSys.quest[index].questGoal.Count;
                for (int i = 0; i < numberOfGoals; i++)
                {
                    if (index == questSys.niveau && i <= questSys.etape)
                    {
                        descObj[i].SetActive(true);
                    }
                    else if (index == questSys.niveau && i > questSys.etape)
                    {
                        descObj[i].SetActive(false);
                    }
                    /*else if(index == questSys.niveau && i == questSys.etape)
                    {
                        descObj[i].SetActive(false);
                    }*/
                }
            }
        } else {
            //buttons.ClosedArrow();

            foreach (GameObject go in descObj)
            {
                if (go.activeInHierarchy == true)
                {
                    go.SetActive(false);
                    //Debug.Log("coucou");
                }

            }
        }
    }
    
    private void OnEnable()
    {
        SynchroniseText();
        HideProgress();
        //questSys.lvlTracker = questSys.niveau;
    }


    public void QuestChanged()
    {
        questSys.niveau = lvlID;
    }
    
}
