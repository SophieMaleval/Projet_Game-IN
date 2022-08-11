﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllosiusDev;
using System;

namespace AllosiusDev.TranslationSystem
{
    public class LangueManager : Singleton<LangueManager>
    {
        #region Events

        public event Action onLangageUpdated;

        #endregion

        #region UnityInspector

        [SerializeField] private LocalisationManager.Langage startLangage;

        #endregion

        #region Behaviour

        protected override void Awake()
        {
            base.Awake();

            ChangeLangage(startLangage);
        }

        public LocalisationManager.Langage GetCurrentLangage()
        {
            return LocalisationManager.currentLangage;
        }

        public void ChangeLangage(LocalisationManager.Langage newLangage)
        {
            LocalisationManager.SetCurrentLangage(newLangage);
            Debug.Log(LocalisationManager.currentLangage);
            if (onLangageUpdated != null)
                onLangageUpdated();
        }

        public string Translate(string key, TypeDictionary typeDictionary)
        {
            string translatedText = LocalisationManager.GetLocalisedValue(key, typeDictionary);

            if (translatedText.Contains("[PLAYER]"))
            {
                translatedText = translatedText.Replace("[PLAYER]", GameManager.Instance.player.PlayerName);
            }

            return translatedText;
        }

        #endregion
    }
}