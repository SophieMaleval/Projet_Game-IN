using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionManager : MonoBehaviour
{
    public GameObject QuestionBox;

    public GameObject Prop1;
    public GameObject Prop2;
    public GameObject Prop3;
    public GameObject Prop4;

    public int NbProp;  //ca va de deux a 4

    public int NbPropSelected; // La ou le curseur se met 

    public bool BoxIsActivated;



 
    void Start()
    {

        Prop3.SetActive(false);
        Prop4.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // écrire une fonction quand on a nb prop = 2 ça desactive les autres propositions
        // Ecrire une fonction avec les sprites qui s'acive et se desactive en fonction de la proposition sélectionné 
        if (NbProp == 1 )
        {
            Prop1.SetActive(true);
            Prop2.SetActive(false);
            
        }

          if (NbProp == 2 )
        {
            Prop2.SetActive(true);
            Prop1.SetActive(true);
            
        }
        


        if (BoxIsActivated == true &&  Input.GetKeyUp(KeyCode.Return ))

        {
            // checker si on a besoin de reset 
            BoxIsActivated =  false; // selon le nbPropSelected ça va avoir un impact sur les prochaines lignes de dialogues s line et e line 
        }


        if (BoxIsActivated == true && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            NbProp = NbProp-1;
            if (NbProp< 1){

                NbProp = 4;
            }
        }
        
        if (BoxIsActivated == true && Input.GetKeyUp(KeyCode.RightArrow))
        {
            NbProp = NbProp + 1;

             if (NbProp > 4){

                NbProp = 1 ;
            }
        }
        
    

        
    }
}
