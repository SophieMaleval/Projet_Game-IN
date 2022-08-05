﻿using System.Collections.Generic;

namespace AllosiusDev.TranslationSystem
{
    /// <summary>
    /// Point d'entrée vers les traductions
    /// </summary>
    public class LocalisationManager
    {
        /// <summary>
        /// Liste des langues gérées
        /// </summary>
        public enum Langage
        {
            Francais,
            Anglais,
        }

        public static Langage currentLangage = Langage.Anglais;

        /// <summary>
        /// Dictionnaire chargé en mémoire avec la langue utilisée
        /// </summary>
        private static Dictionary<string, string> localDico;
        
        /// <summary>
        /// Booléen permettant de savoir si la langue est chargée
        /// </summary>
        public static bool isInit;

        /// <summary>
        /// Fonction permettant de charger le dictionnaire en fonction de la langue de l'utilisateur
        /// </summary>
        public static void Init(TypeDictionary typeDictionary)
        {
            JSONLoader json;

            switch (currentLangage)
            {
                case Langage.Francais:
                    json = JSONLoader.LoadJSON("fr", typeDictionary);
                    break;
                case Langage.Anglais:
                    json = JSONLoader.LoadJSON("en", typeDictionary);
                    break;
                default:
                    json = JSONLoader.LoadJSON("en", typeDictionary);
                    break;
            }

            localDico = json.GetDictionaryValues();

            isInit = true;
        }

        public static void SetCurrentLangage(Langage newLangage)
        {
            currentLangage = newLangage;
            isInit = false;
        }

        /// <summary>
        /// Fonction permettant de récupérer une chaine de charactère dans la langue chargée en dictionnaire (en mémoire)
        /// </summary>
        /// <param name="key">Clé de recherche du texte à récupérer</param>
        /// <returns>texte traduit</returns>
        public static string GetLocalisedValue(string key, TypeDictionary typeDictionary, bool mustInitialized = true)
        {
            if (mustInitialized)
            {
                if (!isInit)
                {
                    Init(typeDictionary);
                }
            }
            else
            {
                Init(typeDictionary);
            }

            string value;

            localDico.TryGetValue(key, out value);

            if(value == null)
            {
                value = key;
            }

            return value;
        }
		
		//A appeler de cette manière partout dans le jeu LocalisationManager.GetLocalisedValue("ma_clé")
    }

    public enum TypeDictionary
    {
        Default,
        GeneralsUI,
        InformationsPanelsTexts,
        PopUps,
        InventoryItems,
        Dialogues,
        Quests,
    }
}