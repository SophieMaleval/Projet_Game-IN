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
    public bool isActivated = false;
    public PlayableDirector debutCS;
    public PlayableDirector finCS;

    public string debutSequence, finSequence;
    void OnEnable()
    {
       // camPrincipal = GameObject.Find(camera1);
      //  camCutscene = GameObject.Find(camera2);
        camCutscene.SetActive(false);
      camCutscene.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3 ;
        debutCS = GameObject.Find(debutSequence).GetComponent<PlayableDirector>();
        finCS = GameObject.Find(finSequence).GetComponent<PlayableDirector>();
        debutCS.enabled = false;
        finCS.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchCam();
        Lighting();
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
