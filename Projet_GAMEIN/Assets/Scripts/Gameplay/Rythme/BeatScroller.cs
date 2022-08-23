using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    #region Fields

    //private NotesSpawner notesSpawner; 


    #endregion

    #region Properties

    public AudioSource musicSource { get; protected set; }

    public bool canPlay { get; set; }

    public float tempDureeMusique { get; set; }

    #endregion

    #region UnityInspector

    //public AudioSource music;

    public AudioData music;

    public float beatTempo;
    public float dureeMusique;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Awake()
    {
        beatTempo = beatTempo / 60f;
        canPlay = true;
        /*notesSpawner = GameObject.Find("NoteHolder").GetComponent<NotesSpawner>();*/
    }

    private void OnEnable()
    {
        StartCoroutine(GuyterHiro());
        musicSource = AudioController.Instance.PlayAudio(music);
        InvokeRepeating("DecreaseTime", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GuyterHiro()
    {
        while(dureeMusique > 0)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            yield return null;
        }
        
        yield return new WaitForSeconds(0.5f);

        if(canPlay)
        {
            //notesSpawner.stopProcess = true;
            RhythmManager.instance.Results();
            Debug.Log("Finito pipo");
        }

       
    }

    void DecreaseTime()
    {
        if(dureeMusique > 0)
        {
            dureeMusique -= 1;
        }

    }

    #endregion
}
