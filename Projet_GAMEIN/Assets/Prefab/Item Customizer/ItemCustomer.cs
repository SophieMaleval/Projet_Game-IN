using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Item - Custome Character")]
public class ItemCustomer : ScriptableObject
{
    [Header ("Customisation de Personnage")]
    public Sprite DisplayCustomisation ;
    public RuntimeAnimatorController Animator ; 
}
