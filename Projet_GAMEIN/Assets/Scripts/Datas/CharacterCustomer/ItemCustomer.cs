using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemCustomer", menuName = "Village/Item - Custome Character")]
public class ItemCustomer : ScriptableObject
{
    [Header ("Customisation de Personnage")]
    public Sprite DisplayCustomisation ;
    public RuntimeAnimatorController Animator ; 
}
