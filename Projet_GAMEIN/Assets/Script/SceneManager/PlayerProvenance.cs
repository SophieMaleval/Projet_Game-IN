using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvenance : MonoBehaviour
{
    public bool ProviensCharacterCustomer ;
    public bool ProviensGameIn ;
    public bool ProviensMain ;
    public bool ProviensEArtsup ;
    public bool ProviensOrdiRetro ;
    public bool ProviensNacon ;
    public bool ProviensAccidentalQueen ;
    public bool ProviensCouchGameCrafter ;
    public bool ProviensBPF ;

    public void SetAllBoolToFalse()
    {
        ProviensCharacterCustomer = false ;
        ProviensGameIn = false ;
        ProviensMain = false ;
        ProviensEArtsup = false ;
        ProviensOrdiRetro = false ;
        ProviensNacon = false ;
        ProviensAccidentalQueen = false ;
        ProviensCouchGameCrafter = false ;
        ProviensBPF = false ;
    }
}
