using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllosiusDev;
using System;
using Village.EncyclopaediaMenu;
using AllosiusDev.Utils;

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

        public string Translate(string key, TypeDictionary typeDictionary, bool colorCode = true)
        {
            string translatedText = LocalisationManager.GetLocalisedValue(key, typeDictionary);

            if (translatedText.Contains("[PLAYER]"))
            {
                translatedText = translatedText.Replace("[PLAYER]", GameManager.Instance.player.PlayerName);
            }

            if(colorCode)
            {
                List<LocationStatus> locationStatuses = GameManager.Instance.locationsList.GetStatuses();
                if (locationStatuses.Count > 0)
                {
                    for (int i = 0; i < locationStatuses.Count; i++)
                    {
                        if (locationStatuses[i].GetLocation().locationName != "" && translatedText.Contains(locationStatuses[i].GetLocation().locationName))
                        {
                            Debug.Log("Replace : " + locationStatuses[i].GetLocation().locationName);
                            string locationHexColor = "#" + AllosiusDevUtilities.GetStringFromColor(locationStatuses[i].GetLocation().zone.zoneColor);
                            string textReplace = "<color=" + locationHexColor + ">" + locationStatuses[i].GetLocation().locationName + "</color>";
                            translatedText = translatedText.Replace(locationStatuses[i].GetLocation().locationName, textReplace);
                        }
                    }
                }
            }
            

            return translatedText;
        }

        #endregion
    }
}
