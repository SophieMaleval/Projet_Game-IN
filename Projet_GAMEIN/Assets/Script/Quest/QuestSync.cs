using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestSync : MonoBehaviour
{
    public QuestSys questSys;
    public Checker checker;
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

    private void Awake()
    {
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
        descObj = GameObject.FindGameObjectsWithTag(descName);
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

                if (index < questSys.lvlTracker)
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
        title = this.gameObject;
        //descObj = GameObject.FindGameObjectsWithTag(descName); //
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
