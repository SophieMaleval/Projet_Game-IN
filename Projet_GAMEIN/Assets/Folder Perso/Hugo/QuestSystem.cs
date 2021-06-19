using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystem : MonoBehaviour
{
    public Text textQuest;
    QuestStep questStep;
    public int stepCount;
    // Start is called before the first frame update
    void Start()
    {
        questStep = this.gameObject.GetComponent<QuestStep>();
        textQuest.text = questStep.questStep[0];
        stepCount = 0;
    }

    void Update() 
    {
        textQuest.text = questStep.questStep[stepCount];
    }
}
