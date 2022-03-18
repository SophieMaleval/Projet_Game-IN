using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSpawner : MonoBehaviour
{
    public GameObject left, right, up, down;
    public int notesSpawned, notesCounter;
    public Transform parent;
    public BeatScroller bS;

    public bool stopProcess;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform;
        /*for (int i = 0; i < numberOfNotes; i++)
        {
            Instantiate(left, left.transform.position, left.transform.rotation, parent);
            Instantiate(right, right.transform.position, right.transform.rotation, parent);
            Instantiate(up, up.transform.position, up.transform.rotation, parent);
            Instantiate(down, down.transform.position, down.transform.rotation, parent);
        }*/
        //Random rand = new Random();
        InvokeRepeating("LeftArrow", 1f, Random.Range(1, 4));
        InvokeRepeating("RightArrow", 2f, Random.Range(1, 4));
        InvokeRepeating("UpArrow", 3f, Random.Range(1, 5));
        InvokeRepeating("DownArrow", 4f, Random.Range(1, 6));
    }

    private void Awake()
    {
        bS = GameObject.Find("NoteHolder").GetComponent<BeatScroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notesSpawned <= 0)
        {
            //stopProcess = true;
            if(stopProcess == true)
            {
                FinitoPipo();
            }            
        }
    }
    void FinitoPipo()
    {
        if (stopProcess)
        {
            stopProcess = false;
            CancelInvoke();
            Debug.Log("Stahp");
        }
        
    }
    void LeftArrow()
    {
        Instantiate(left, left.transform.position, left.transform.rotation, parent);
        notesSpawned--;
        notesCounter++;
    }

    void RightArrow()
    {
        Instantiate(right, right.transform.position, right.transform.rotation, parent);
        notesSpawned--;
        notesCounter++;
    }

    void UpArrow()
    {
        Instantiate(up, up.transform.position, up.transform.rotation, parent);
        notesSpawned--;
        notesCounter++;
    }

    void DownArrow()
    {
        Instantiate(down, down.transform.position, down.transform.rotation, parent);
        notesSpawned--;
        notesCounter++;
    }
}
