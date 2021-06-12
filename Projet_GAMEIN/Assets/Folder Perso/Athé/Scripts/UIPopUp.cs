using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{
  public GameObject BullePoulet;
  public GameObject BulleGateau;
    // Start is called before the first frame update
   
   public void PopUpActivate(){

       BullePoulet.SetActive(true);
    
    
       }

        public void PopUpNone(){

       BullePoulet.SetActive(false);
    
    
       }

        public void PopUpGateauYes(){

       BulleGateau.SetActive(true);
    
    
       }
        public void PopUpGateauNo(){

       BulleGateau.SetActive(false);
    
    
       }
}

