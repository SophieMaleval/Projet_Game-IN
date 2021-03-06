using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class UITextContainer
{
    public List<string> MenuFR ;
    public List<string> MenuEN ;
    
    public List<string> PauseFR ;
    public List<string> PauseEN ;

    public List<string> SettingFR ;
    public List<string> SettingEN ;
    
    public List<string> CustomisationFR ;
    public List<string> CustomisationEN ;

    public List<string> PanelENTFR ;
    public List<string> PanelENTEN ;

    public List<string> ControlFR ;
    public List<string> ControlEN ;

    public List<string> PopUpFR ;
    public List<string> PopUpEN ;  
}


[System.Serializable]
public class QuestionQCMContainer
{
    public List<string> QuestionsReponse = new List<string>();
    public int NuméroRéponse ;
}

[System.Serializable]
public class QCMContainer
{
    public List<QuestionQCMContainer> QCMEnigmeFR1 = new List<QuestionQCMContainer>() ;
    public List<QuestionQCMContainer> QCMEnigmeEN1 = new List<QuestionQCMContainer>() ;

    public List<QuestionQCMContainer> QCMPresFR1 = new List<QuestionQCMContainer>() ;
    public List<QuestionQCMContainer> QCMPresEN1 = new List<QuestionQCMContainer>() ; 

    public List<QuestionQCMContainer> QCMEnigmeFR2 = new List<QuestionQCMContainer>() ;
    public List<QuestionQCMContainer> QCMEnigmeEN2 = new List<QuestionQCMContainer>() ; 
}


public class CSVReader : MonoBehaviour
{
    #region Fields

    [Header ("Adaptation de texte")]
    private PlayerScript PlayerInformations ;

    private string[] Pronoms = new string[]{"une", "un", "un·e"};
    private string[] Terminaisons = new string[]
    { 
        /* féminin, masculin, inclusif */
        "elle", "el", "el·elle",    // 1-3
        "euse", "eur", "eur·euse",  // 4-6
        "rice", "eur", "eur·rice",  // 7-9
        "eure", "eur", "eur·e", // 10-12
        "ière", "ier", "ier·ière",  //13-15
        "ère", "er", "er·ère",  // 16-18
        "ienne", "ien", "ien·ienne" // 19-21
    };

    #endregion

    #region UnityInspector

    [Header ("Fichier Texte")]
    public TextAsset DialogDataFR ;
    public TextAsset DialogDataEN ;

    public TextAsset QCMData ;

    public TextAsset UIDataText ;

    public TextAsset QuestText ;

    public TextAsset PannelENTText ;

    [Header ("Texte UI")]
    public UITextContainer UIText = new UITextContainer();

    [Header ("QCM")]
    public QCMContainer QCMCont = new QCMContainer();

    [Header ("Texte Quest")]
    public QuestSys QuestManager ;    

    [Header ("PannelENT")]
    public List<UIPanelENTContainer> TextUIPanneauxENT = new List<UIPanelENTContainer>();

    #endregion

    #region Behaviour

    private void Awake() 
    {
        if(PlayerInformations == null)
            PlayerInformations = transform.parent.GetComponent<PlayerScript>();
    }
    void Start()
    {
        ReadUICSV();
        if(QuestManager != null) ReadQuestCSV();
        if(PannelENTText != null) ReadTextPannelENT();
        if(QCMData != null) ReadQCMCSV() ;
    }


    public void SetUpDialogueAdhérent()
    {
        ReaderDialogCSV(DialogDataFR, GetComponent<PlayerDialogue>().myDialogueAdhérentFR);
        ReaderDialogCSV(DialogDataEN, GetComponent<PlayerDialogue>().myDialogueAdhérentEN);        
    }

    void ReadUICSV()
    {
        string[] LineData = UIDataText.text.Split(new string[] { "\n" }, StringSplitOptions.None) ; // Data correspond à chaque Ligne

        for (int LD = 0; LD < LineData.Length; LD++)
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

                        if(Data[0] == "Pause FR")    UIText.PauseFR.Add(Data[D]) ;
                        if(Data[0] == "Pause EN")    UIText.PauseEN.Add(Data[D]) ; 

                        if(Data[0] == "Setting FR")    UIText.SettingFR.Add(Data[D]) ;
                        if(Data[0] == "Setting EN")    UIText.SettingEN.Add(Data[D]) ; 

                        if(Data[0] == "Customisation FR")    UIText.CustomisationFR.Add(Data[D]) ;
                        if(Data[0] == "Customisation EN")    UIText.CustomisationEN.Add(Data[D]) ; 
                    
                        if(Data[0] == "Panneaux ENT FR")    UIText.PanelENTFR.Add(Data[D]);
                        if(Data[0] == "Panneaux ENT EN")    UIText.PanelENTEN.Add(Data[D]);
                    
                        if(Data[0] == "Control FR")    UIText.ControlFR.Add(Data[D]);
                        if(Data[0] == "Control EN")    UIText.ControlEN.Add(Data[D]);

                        if(Data[0] == "PopUp FR")    UIText.PopUpFR.Add(Data[D]);
                        if(Data[0] == "PopUp EN")    UIText.PopUpEN.Add(Data[D]);   
                    }
                }
            }                
        }

        // Add UIText du Pannel des ENT au PannelENTManager
      /*  if(PlayerInformations != null)
        {
            PannelENTManager ManagerPannelENT = PlayerInformations.PannelENTUIIndestructible.GetComponent<PannelENTManager>() ;
            ManagerPannelENT.UIPanelENTFR = UIText.PanelENTFR;
            ManagerPannelENT.UIPanelENTEN = UIText.PanelENTEN;            
        }*/
    }

    void ReadQCMCSV()
    {
        string[] LineData = QCMData.text.Split(new string[] { "\n" }, StringSplitOptions.None) ; // Data correspond à chaque Ligne

        for (int LD = 1; LD < LineData.Length; LD++)
        {
            QuestionQCMContainer QCMQR = new QuestionQCMContainer();

            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None) ; // Data correspond à chaque case

            if(Data[0] != "")
            {
                for (int D = 0; D < Data.Length; D++)
                {
                    if(Data[D] == null)Debug.Log(Data[D]);
                    if(D == 2)    QCMQR.NuméroRéponse = int.Parse(Data[D]) ;

                    int DataNombRep = int.Parse(Data[3]) ;

                    if(D > 3 && (D < 4 + DataNombRep + 1))    QCMQR.QuestionsReponse.Add(Data[D]) ;
                }

                if(Data[0] == "QCM 1 FR")    QCMCont.QCMEnigmeFR1.Add(QCMQR);
                if(Data[0] == "QCM 1 EN")    QCMCont.QCMEnigmeEN1.Add(QCMQR);

                if(Data[0] == "PRES 1 FR")    QCMCont.QCMPresFR1.Add(QCMQR);
                if(Data[0] == "PREs 1 EN")    QCMCont.QCMPresEN1.Add(QCMQR);

                if(Data[0] == "QCM 2 FR")    QCMCont.QCMEnigmeFR2.Add(QCMQR);
                if(Data[0] == "QCM 2 EN")    QCMCont.QCMEnigmeEN2.Add(QCMQR);  
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

                InfoDiag.Entreprise = Data[0];
                InfoDiag.Name = Data[1];
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

        if(CharacterAsReplaced != null)
        {
            string WithName = CharacterAsReplaced.Replace("§", PlayerInformations.PlayerName) ;   

            string WithPronom = WithName.Replace("¤", Pronoms[PlayerInformations.PlayerSexualGenre]) ;   

            string WithTerms1 = WithPronom.Replace("000001", Terminaisons[PlayerInformations.PlayerSexualGenre + 0]) ;     // "elle", "el", "el·elle"
            string WithTerms2 = WithTerms1.Replace("000010", Terminaisons[PlayerInformations.PlayerSexualGenre + 3]) ;            // "euse", "eur", "eur·euse"
            string WithTerms3 = WithTerms2.Replace("000011", Terminaisons[PlayerInformations.PlayerSexualGenre + 6]) ;            // "rice", "eur", "eur·rice"
            string WithTerms4 = WithTerms3.Replace("000100", Terminaisons[PlayerInformations.PlayerSexualGenre + 9]) ;            // "eure", "eur", "eur·e"
            string WithTerms5 = WithTerms4.Replace("000101", Terminaisons[PlayerInformations.PlayerSexualGenre + 12]) ;           // "ière", "ier", "ier·ière"
            string WithTerms6 = WithTerms5.Replace("000110", Terminaisons[PlayerInformations.PlayerSexualGenre + 15]) ;           // "ère", "er", "er·ère"
            string WithTerms7 = WithTerms6.Replace("000111", Terminaisons[PlayerInformations.PlayerSexualGenre + 18]) ;           // "ienne", "ien", "ien·ienne"
           // string WithTerms8 = WithPronom.Replace("001000", Terminaisons[PlayerInformations.PlayerSexualGenre + 21]) ;           // 

            DataCorrected = WithTerms7 ;
        }

        return DataCorrected ;
    }


    void ReadQuestCSV()
    {
        string[] LineData = QuestText.text.Split(new string[] { "\n" }, StringSplitOptions.None) ; //Line Data correspond à chaque Ligne
        int QuestCode = 0;
        for (int LD = 1; LD < LineData.Length; LD++)
        {
            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None) ; //Data correspond à chaque Case 

            bool QuestAdd = false ;

            if(Data[0] != "")
            {
                QuestCt NewQuestCt = new QuestCt() ;
                NewQuestCt.questCode = QuestCode ;

                NewQuestCt.intitule = Data[0] ; 
                NewQuestCt.questTitle = Data[1] ;
                NewQuestCt.QuestGoalLength = int.Parse(Data[2]) ; 

                for (int D = 3; D < Data.Length; D++)
                {
                    if(D < (NewQuestCt.QuestGoalLength + 3)) NewQuestCt.questGoal.Add(Data[D]) ;
                }


                if(QuestManager.QuestFR.Count == QuestManager.QuestEN.Count && QuestAdd == false)
                {
                    QuestAdd = true ;
                    QuestManager.QuestFR.Add(NewQuestCt);                   
                }
                if(QuestManager.QuestFR.Count == QuestManager.QuestEN.Count + 1 && QuestAdd == false)
                {
                    QuestAdd = true ;
                    QuestManager.QuestEN.Add(NewQuestCt);  
                } 
            } else {
                QuestCode ++ ;
            } 
        }
    }


    void ReadTextPannelENT()
    {
        string[] LineData = PannelENTText.text.Split(new string[] { "¤" }, StringSplitOptions.None) ; // Data correspond à chaque Ligne

        for (int LD = 1; LD < LineData.Length; LD++)
        {
            string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None) ; // Data correspond à chaque case

            if(Data[0] != "-")
            {
                UIPanelENTContainer NewTextPannel = new UIPanelENTContainer() ;

                string[] Data0Split = Data[0].Split(new string[] { "\n"}, StringSplitOptions.None);
                NewTextPannel.NomEntrprise = Data0Split[1] ;
               
                NewTextPannel.DescriptionEntreprise = Data[1] ;                    

                NewTextPannel.Valeurs = Data[2] ;

                NewTextPannel.TitreDernièreProd = Data[3] ;
                NewTextPannel.DescriptionDernièreProd = Data[4] ;

                if(Data[5] != null && Data[6] != null)
                {
                    NewTextPannel.TitreAvantDernièreProd = Data[5] ;
                    NewTextPannel.DescriptionAvantDernièreProd = Data[6] ;                    
                }

                if(VerificationIfContainerNull(NewTextPannel) && Data0Split[1] != "-")
                    TextUIPanneauxENT.Add(NewTextPannel);
            }                
        }
    }

    bool VerificationIfContainerNull(UIPanelENTContainer ContainerToBeChecked)
    {
        bool ThiContainerState = false;

        if(ContainerToBeChecked.NomEntrprise != "") ThiContainerState = true ;
        if(ContainerToBeChecked.TitreDernièreProd != "") ThiContainerState = true ;
        if(ContainerToBeChecked.TitreAvantDernièreProd != "") ThiContainerState = true ;

        return ThiContainerState ;
    }

    #endregion
}