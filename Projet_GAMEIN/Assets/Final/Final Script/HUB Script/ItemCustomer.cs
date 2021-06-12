﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Item - Custome Character")]
public class ItemCustomer : ScriptableObject
{
    [Header ("Customisation de Personnage")]
    public Sprite DisplayCustomisation ;
    public Sprite Face ;
    public Sprite Profil ;
    public Sprite Dos ;

    [Header ("Animation In Game")]
    public RuntimeAnimatorController IdleFace ;
    public RuntimeAnimatorController IdleProfil ;
    public RuntimeAnimatorController IdleDos ;
    [Space]
    public RuntimeAnimatorController MoveFace ;
    public RuntimeAnimatorController MoveProfil ;
    public RuntimeAnimatorController MoveDos ;
}
