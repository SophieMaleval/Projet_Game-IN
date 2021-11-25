using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public GameObject textQuest;
    public Text a;
    QuestStep questStep;
    public int stepCount;
    public GameObject lesGens;

    Radio radio;
    // Start is called before the first frame update
    void Start()
    {
        radio = this.gameObject.GetComponent<Radio>();
        textQuest = GameObject.Find("textQuest");
        a = textQuest.GetComponent<Text>();
        a.text = textQuest.GetComponent<Text>().text;
        questStep = this.gameObject.GetComponent<QuestStep>();
        a.text = questStep.questStep[0];
        //stepCount = 0;
    }
    public void findLesGens()
    {
        lesGens = GameObject.Find("les gens");
        lesGens.SetActive(false);
    }
    public void findtestQuest()
    {
        textQuest = GameObject.Find("textQuest");
        a = textQuest.GetComponent<Text>();
        a.text = textQuest.GetComponent<Text>().text;
    }
    void Update() 
    {
        a.text = questStep.questStep[stepCount];
        if(stepCount >= 17)
        {
            Party();
        }
        if(stepCount == 9){radio.questOn = true;}else{radio.questOn = false;}
    }
    public void Party()
    {
        lesGens.SetActive(true);
    }
}
