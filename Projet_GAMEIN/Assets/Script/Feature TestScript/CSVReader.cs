using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UITextContainer
{
    public List<string> MenuFR ;
    public List<string> MenuEN ;
    
    public List<string> SettingFR ;
    public List<string> SettingEN ;
    
    public List<string> CustomisationFR ;
    public List<string> CustomisationEN ;
}


public class CSVReader : MonoBehaviour
{
    [Header ("Fichier Texte")]
    public TextAsset DialogDataFR ;
    public TextAsset DialogDataEN ;

    public TextAsset UIDataText ;

    public TextAsset QuestText ;

    [Header ("Texte UI")]
    public UITextContainer UIText = new UITextContainer();

    [Header ("Adaptation de texte")]
    private PlayerScript PlayerInformations ;

    private string[] Pronoms = new string[]{"un", "une", "un.e"};
    private string[] Terminaisons = new string[]{"eur", "rice", "eurice"};

    [Header ("Texte Quest")]
    public QuestSys QuestManager ;    


    private void Awake() {
        PlayerInformations = transform.parent.GetComponent<PlayerScript>();
    }
    void Start()
    {
        ReadUICSV();
        if(QuestManager !=null) ReadQuestCSV();
    }


    public void SetUpDialogueAdhérent()
    {
        ReaderDialogCSV(DialogDataFR, GetComponent<PlayerDialogue>().myDialogueAdhérentFR);
        ReaderDialogCSV(DialogDataEN, GetComponent<PlayerDialogue>().myDialogueAdhérentEN);        
    }

    void ReadUICSV()
    {
        string[] LineData = UIDataText.text.Split(new string[] { "\n" }, StringSplitOptions.None) ; // Data correspond à chaque Ligne

        for (int LD = 1; LD < LineData.Length; LD++)
        {
            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None) ; // Data correspond à chaque case

            if(Data[0] != "")
            {
                for (int D = 1; D < Data.Length; D++)
                {
                    if(Data[D] != "")
                    {
                        if(Data[0] == "Menu FR")    UIText.MenuFR.Add(Data[D]) ;
                        if(Data[0] == "Menu EN")    UIText.MenuEN.Add(Data[D]) ;

                        if(Data[0] == "Setting FR")    UIText.SettingFR.Add(Data[D]) ;
                        if(Data[0] == "Setting EN")    UIText.SettingEN.Add(Data[D]) ; 

                        if(Data[0] == "Customisation FR")    UIText.CustomisationFR.Add(Data[D]) ;
                        if(Data[0] == "Customisation EN")    UIText.CustomisationEN.Add(Data[D]) ; 
                    }
                }
            }                
        }
    }

    void ReaderDialogCSV(TextAsset DialogDataLanguage, List<DialogueContainer> TargetList) 
    {
        TargetList.Clear();
        string[] LineData = DialogDataLanguage.text.Split(new string[] { "\n" }, StringSplitOptions.None); // LineData correspond à chaque Ligne

        for (int LD = 1; LD < LineData.Length; LD++)
        {         
            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None); // Data correspond à chaque case    

            if(Data[0] != "")
            {
                DialogueContainer InfoDiag = new DialogueContainer();
                InfoDiag.Entreprise = ReplaceCharacter(Data[0]);
                InfoDiag.Name = ReplaceCharacter(Data[1]);
                InfoDiag.OpeningDialogue = ReplaceCharacter(Data[2]);
                InfoDiag.CloseDiscussion = ReplaceCharacter(Data[3]);
                    
                InfoDiag.Question1 = ReplaceCharacter(Data[4]);
                InfoDiag.Question2 = ReplaceCharacter(Data[5]);
                InfoDiag.Question3 = ReplaceCharacter(Data[6]);
                InfoDiag.Question4 = ReplaceCharacter(Data[7]);
                InfoDiag.Question5 = ReplaceCharacter(Data[8]);
                InfoDiag.Question6 = ReplaceCharacter(Data[9]);
                InfoDiag.Question7 = ReplaceCharacter(Data[10]);
                InfoDiag.Question8 = ReplaceCharacter(Data[11]);
                InfoDiag.Question9 = ReplaceCharacter(Data[12]);
                InfoDiag.Question10 = ReplaceCharacter(Data[13]);
                InfoDiag.Aurevoir = ReplaceCharacter(Data[14]);

                    

                InfoDiag.Dialogue1 = ReplaceCharacter(Data[15]);
                InfoDiag.Dialogue2 = ReplaceCharacter(Data[16]);
                InfoDiag.Dialogue3 = ReplaceCharacter(Data[17]);
                InfoDiag.Dialogue4 = ReplaceCharacter(Data[18]);
                InfoDiag.Dialogue5 = ReplaceCharacter(Data[19]);
                InfoDiag.Dialogue6 = ReplaceCharacter(Data[20]);
                InfoDiag.Dialogue7 = ReplaceCharacter(Data[21]);
                InfoDiag.Dialogue8 = ReplaceCharacter(Data[22]);
                InfoDiag.Dialogue9 = ReplaceCharacter(Data[23]);    
                InfoDiag.Dialogue10 = ReplaceCharacter(Data[24]);    
                InfoDiag.Dialogue11 = ReplaceCharacter(Data[25]);    
                InfoDiag.Dialogue12 = ReplaceCharacter(Data[26]);    
                InfoDiag.Dialogue13 = ReplaceCharacter(Data[27]);    
                InfoDiag.Dialogue14 = ReplaceCharacter(Data[28]);    
                InfoDiag.Dialogue15 = ReplaceCharacter(Data[29]);    
                InfoDiag.Dialogue16 = ReplaceCharacter(Data[30]);    
                InfoDiag.Dialogue17 = ReplaceCharacter(Data[31]);    
                InfoDiag.Dialogue18 = ReplaceCharacter(Data[32]);    
                InfoDiag.Dialogue19 = ReplaceCharacter(Data[33]);    
                InfoDiag.Dialogue20 = ReplaceCharacter(Data[34]);    


                TargetList.Add(InfoDiag);
            }
        }
    }

    string ReplaceCharacter(string CharacterAsReplaced)
    {
        string DataCorrected = "";

        string WithName = CharacterAsReplaced.Replace("§", PlayerInformations.PlayerName) ;   

        string WithPronom = WithName.Replace("¤", Pronoms[PlayerInformations.PlayerSexualGenre]) ;   
        string WithTerms = WithPronom.Replace("~", Terminaisons[PlayerInformations.PlayerSexualGenre]) ;   

        DataCorrected = WithTerms ;
        return DataCorrected ;
    }


    void ReadQuestCSV()
    {
        string[] LineData = QuestText.text.Split(new string[] { "\n" }, StringSplitOptions.None) ; //Line Data correspond à chaque Ligne
        int QuestCode = 0;
        for (int LD = 1; LD < LineData.Length; LD++)
        {
            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None) ; //Data correspond à chaque Case 

            if(Data[0] != "")
            {
                QuestCt NewQuestCt = new QuestCt() ;
                NewQuestCt.questCode = QuestCode ;
                string[] QuestGoal ;

                for (int i = 0; i < Data.Length; i++)
                {
                    if(i == 0) NewQuestCt.intitule = Data[0] ; 
                    if(i == 1) NewQuestCt.questTitle = Data[1] ; 

                    if(i > 1) NewQuestCt.questGoal.
                }

            } else {
                QuestCode ++ ;
            } 
        }
    }





}