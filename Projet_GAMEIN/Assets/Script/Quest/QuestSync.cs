using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestSync : MonoBehaviour
{
    public QuestSys questSys;
    public GameObject title;
    public TextMeshProUGUI[] description;
    [SerializeField] int index;
    [SerializeField] int lvlID;
    public int currentStep;
    [SerializeField] string descName;
    //public List<string> steps = new List<string>();
    public string[] steps;
    
    int numberOfGoals;
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
                //description[i].fontStyle = FontStyles.Strikethrough;
            }
        }
    }
    //public void SynchroniseDesc()
    //{
        //steps[steps.Length] = questSys.quest[index].questGoal[questSys.quest[index].questGoal.Length -1];
        //steps[steps.Count] = questSys.quest[index].questGoal[];
        //description = GameObject.FindGameObjectsWithTag(descName);
        //foreach (GameObject tagged in description)
        /*if (tagged.activeInHierarchy == true)
        {
            steps.AddRange(tagged.GetComponentInChildren<TextMeshProUGUI>().text);
            //tagged.GetComponentInChildren<TextMeshProUGUI>().text = questSys.quest[index].questGoal[indexLvl];
        }*/
    //}

    private void OnEnable()
    {
        SynchroniseText();
    }

    public void QuestChanged()
    {
        questSys.niveau = lvlID;
        //questSys.quest[lvlID].questGoal[] = currentStep;
    }
    void Start()
    {
        //steps = questSys.quest[index].questGoal.ToList();
    }
    
    void Update()
    {
       
    }
}
