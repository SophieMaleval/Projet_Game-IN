using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public float dureeMusique;
    //private NotesSpawner notesSpawner; 
    // Start is called before the first frame update
    void Awake()
    {
        beatTempo = beatTempo / 60f;
        /*notesSpawner = GameObject.Find("NoteHolder").GetComponent<NotesSpawner>();*/
    }

    private void OnEnable()
    {
       StartCoroutine(GuyterHiro());
       RhythmManager.instance.Music();
       //InvokeRepeating("DecreaseTime", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GuyterHiro()
    {
        while(dureeMusique > 0)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);
        //notesSpawner.stopProcess = true;
        RhythmManager.instance.Results();        
        Debug.Log("Finito pipo");
    }

    /*void DecreaseTime()
    {
        if(dureeMusique > 0)
        {
            dureeMusique -= 1;
        }
        if(notesSpawner.notesSpawned <= 0)
        {
            notesSpawner.stopProcess = true;
        }
    }*/
}
