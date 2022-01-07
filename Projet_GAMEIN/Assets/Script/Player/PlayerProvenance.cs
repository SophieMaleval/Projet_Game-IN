using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerProvenance : MonoBehaviour
{
    public bool ProviensCharacterCustomer = true ;
    public bool ProviensGameIn = false ;
    public bool ProviensMain = false ;

    public bool ProviensCouchGameCrafter = false ;


    public void SetAllBoolToFalse()
    {
        ProviensCharacterCustomer = false ;
        ProviensGameIn = false ;
        ProviensMain = false ;

        ProviensCouchGameCrafter = false ;

    }

    

}
