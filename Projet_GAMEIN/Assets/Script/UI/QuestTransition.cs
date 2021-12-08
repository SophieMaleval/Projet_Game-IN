using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class QuestTransition : MonoBehaviour
{
    public GameObject[] questSlot;
    public GameObject fleche;
    public Sprite unclickedFleche;
    public Sprite droppedFleche;
    public string steps;

    private void Start()
    {
        unclickedFleche = fleche.GetComponent<Image>().sprite;
        //fleche = GameObject.Find(nomFleche);
        questSlot = GameObject.FindGameObjectsWithTag(steps);
        foreach (GameObject tagged in questSlot)
        {
            tagged.SetActive(false);
        }
    }
    public void Dropdown()
    {
        foreach (GameObject tagged in questSlot)
        {
            if(tagged.activeInHierarchy == true)
            {
                tagged.SetActive(false);
                fleche.transform.rotation = Quaternion.Euler(0, 0, 90);
                fleche.GetComponent<Image>().sprite = unclickedFleche;
            }
            else
            {
                tagged.SetActive(true);
                fleche.transform.rotation = Quaternion.Euler(0, 0, 0);
                fleche.GetComponent<Image>().sprite = droppedFleche;
            }       
        }
        
    }




}
