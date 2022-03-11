using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI ;

public class PetiteAnnonceManager : MonoBehaviour
{
    [SerializeField] private GameObject PrefabAnnonce ;
    [SerializeField] private PetiteAnnonceContainer InfoTableau ;


    void Start()
    {
        for (int Ai = 0; Ai < InfoTableau.PetiteAnnonceSprite.Count; Ai++)
        {
            InstantiateAnnonce(InfoTableau.PetiteAnnonceSprite[Ai]);
        }
    }

    void InstantiateAnnonce(Sprite SpriteAnnonce)
    {
        GameObject Annonce = Instantiate(PrefabAnnonce);
        Annonce.GetComponent<RectTransform>().sizeDelta = SpriteAnnonce.bounds.size ;
    }


    




    public void SwitchTogglePannelDisplay()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().StartActivity();
        } else {
            gameObject.SetActive(!gameObject.activeSelf);
            GameObject.Find("Player").GetComponent<PlayerMovement>().EndActivity(); 
        }
    }

}
