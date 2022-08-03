using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllosiusDev;
using HeXa;
using System;

public class LangueManager : Singleton<LangueManager>
{
    #region Events

    public event Action onLangageUpdated;

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        ChangeLangage(LocalisationManager.currentLangage);
    }

    public LocalisationManager.Langage GetCurrentLangage()
    {
        return LocalisationManager.currentLangage;
    }

    public void ChangeLangage(LocalisationManager.Langage newLangage)
    {
        LocalisationManager.SetCurrentLangage(newLangage);
        Debug.Log(LocalisationManager.currentLangage);
        if(onLangageUpdated != null)
            onLangageUpdated();
    }

    public string Translate(string key)
    {
        string translatedText = LocalisationManager.GetLocalisedValue(key);
        return translatedText;
    }

    #endregion
}
