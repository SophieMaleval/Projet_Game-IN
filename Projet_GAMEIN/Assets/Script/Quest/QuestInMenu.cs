using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class QuestInMenu : MonoBehaviour
{
    public GameObject[] questSlot;
    public GameObject fleche;
    public Sprite unclickedFleche;
    public Sprite droppedFleche;
    public string steps;
    public GameObject[] otherQ1;
    public GameObject[] otherQ2;
    public GameObject[] otherQ3;
    public string list1, list2, list3;
    public bool isOpen;

    private void Start()
    {
        unclickedFleche = fleche.GetComponent<Image>().sprite;
        questSlot = GameObject.FindGameObjectsWithTag(steps);
        foreach (GameObject tagged in questSlot)
        {
            tagged.SetActive(false);
        }
    }

    private void Awake()
    {
        //otherQ1 = GameObject.FindGameObjectsWithTag(list1);
        //otherQ2 = GameObject.FindGameObjectsWithTag(list2);
       // otherQ3 = GameObject.FindGameObjectsWithTag(list3);
    }
    public void Dropdown()
    {
        isOpen =! isOpen;
        foreach (GameObject tagged in questSlot)
        {
            if(tagged.activeInHierarchy == true && isOpen)
            {
                tagged.SetActive(false);
                Closed();
                ClosedArrow();
            }
            else if (tagged.activeInHierarchy == true && !isOpen)
            {
                tagged.SetActive(true);
                OpenArrow();
            }       
        }       
    }

    public void OpenArrow()
    {
        
            fleche.transform.rotation = Quaternion.Euler(0, 0, 0);
            fleche.GetComponent<Image>().sprite = droppedFleche;
        //Closed();
        isOpen = true;
    }

    public void ClosedArrow()
    {
            fleche.transform.rotation = Quaternion.Euler(0, 0, 90);
            fleche.GetComponent<Image>().sprite = unclickedFleche;
        //Closed();
        isOpen = false;
    }

    public void SelfClosing()
    {
        /*if (isOpen)
        {
            foreach (GameObject tagged in questSlot)
            {
                tagged.SetActive(false);
            }
        }*/

        /*else if (!isOpen)
        {
            foreach (GameObject tagged in questSlot)
            {
                tagged.SetActive(true);
            }
        }*/
        
    }

    public void Closed()
    {
        foreach (GameObject tagged1 in otherQ1)
        {
            if(tagged1.activeInHierarchy == true)
            {
                ClosedArrow();  
                tagged1.SetActive(false);               
            }           
        }
        
        foreach (GameObject tagged2 in otherQ2)
        {
            if (tagged2.activeInHierarchy == true)
            {
                ClosedArrow();
                tagged2.SetActive(false);
            }
        }
            
        foreach (GameObject tagged3 in otherQ3)
        {
            if (tagged3.activeInHierarchy == true)
            {
                ClosedArrow();
                tagged3.SetActive(false);
            }
        }


        
    }
}
