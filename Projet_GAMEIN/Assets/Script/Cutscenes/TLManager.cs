using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class TLManager : MonoBehaviour
{
    //public string camera1;
    //public string camera2;
   // public GameObject camPrincipal;
    public GameObject camCutscene;
    public bool isActivated = false, inQuest = false;
    
    public PlayableDirector debutCS;
    public PlayableDirector finCS;

    public string debutSequence, finSequence;
    public QuestSys questSys;
    public int level;

    private void Awake()
    {
        debutCS = GameObject.Find(debutSequence).GetComponent<PlayableDirector>();
        finCS = GameObject.Find(finSequence).GetComponent<PlayableDirector>();
        debutCS.enabled = false;
        finCS.enabled = false;
        questSys = GameObject.Find("QuestManager").GetComponent<QuestSys>();
    }
    void OnEnable()
    {
        camCutscene.SetActive(false);
        camCutscene.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3 ;
    }

    private void FixedUpdate()
    {
        StepChecker();
    }
    // Update is called once per frame
    void Update()
    {
        if (inQuest)
        {
            SwitchCam();
            Lighting();
        }
    }
    void StepChecker()
    {
        if (questSys.niveau == level)
        {
            inQuest = true;
        }
        else
        {
            inQuest = false;
        }               
    }
    public void Toggle()
    {
        isActivated = !isActivated;
    }
    public void SwitchCam()
    {
        if (isActivated)
        {
            camCutscene.SetActive(true);
          //camPrincipal.SetActive(false);
        }
        else if (!isActivated)
        {
            camCutscene.SetActive(false);
          //camPrincipal.SetActive(true);
        }
    }

    public void Lighting()
    {
        if (isActivated)
        {
            debutCS.enabled = true;
            debutCS.Play();
            finCS.Stop();
            finCS.time = 0;
            finCS.enabled = false;
        }
        else if (!isActivated)
        {
            finCS.enabled = true;
            finCS.Play();
            debutCS.Stop();
            debutCS.time = 0;
            debutCS.enabled = false;
        }
    }


}
