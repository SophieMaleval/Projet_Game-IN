using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            numberOfGoals = questSys.quest[index].questGoal.Count;
            for (int i = 0; i < numberOfGoals; i++)
            {
                description[i].text = questSys.quest[index].questGoal[i];

                if (index < questSys.lvlTracker)
                {
                    description[i].fontStyle = FontStyles.Strikethrough;
                    title.GetComponent<Button>().interactable = false;
                }
                else if (index == questSys.lvlTracker && i < questSys.etape)
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

    private void OnEnable()
    {
        SynchroniseText();
    }

    public void QuestChanged()
    {
        questSys.niveau = lvlID;
    }
    
}
