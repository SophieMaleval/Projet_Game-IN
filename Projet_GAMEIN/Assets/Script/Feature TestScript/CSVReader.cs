using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class DialogueContainer
{
    public string Entreprise ;
    public string Name ;
    public string OpeningDialogue ;        
    public string CloseDiscussion ;        

    /* Les Questions Disponnible */
    public string Question1 ;
    public string Question2 ;
    public string Question3 ;
    public string Question4 ;
    public string Question5 ;
    public string Question6 ;
    public string Question7 ;
    public string Question8 ;
    public string Question9 ;
    public string Question10 ;
    public string Aurevoir ;


    /* Les Dialogue */
    public string Dialogue1 ;
    public string Dialogue2 ;
    public string Dialogue3 ;
    public string Dialogue4 ;
    public string Dialogue5 ;
    public string Dialogue6 ;
    public string Dialogue7 ;
    public string Dialogue8 ;
    public string Dialogue9 ;
    public string Dialogue10 ;
    public string Dialogue11 ;
    public string Dialogue12 ;
    public string Dialogue13 ;
    public string Dialogue14 ;
    public string Dialogue15 ;
    public string Dialogue16 ;
    public string Dialogue17 ;
    public string Dialogue18 ;
    public string Dialogue19 ;
    public string Dialogue20 ;
}



public class CSVReader : MonoBehaviour
{
    public TextAsset DialogData ;

    public List<DialogueContainer> myDialogueAdhérent = new List<DialogueContainer>();

        string[] LineData ;

    void Start()
    {
       ReaderCSV();
    }

    void ReaderCSV() 
    {
        LineData = DialogData.text.Split(new string[] { "\n" }, StringSplitOptions.None); // Data correspond à chaque Ligne

        for (int LD = 0; LD < LineData.Length; LD++)
        {
            if(LD != 0)
            {              
                string[] Data = LineData[LD].Split(new string[] { ";" }, StringSplitOptions.None); // Data correspond à chaque case    

                if(Data[0] != "")
                {
                    DialogueContainer InfoDiag = new DialogueContainer();
                    InfoDiag.Entreprise = Data[0];
                    InfoDiag.Name = Data[1];
                    InfoDiag.OpeningDialogue = Data[2];
                    InfoDiag.CloseDiscussion = Data[3];
                    
                    InfoDiag.Question1 = Data[4];
                    InfoDiag.Question2 = Data[5];
                    InfoDiag.Question3 = Data[6];
                    InfoDiag.Question4 = Data[7];
                    InfoDiag.Question5 = Data[8];
                    InfoDiag.Question6 = Data[9];
                    InfoDiag.Question7 = Data[10];
                    InfoDiag.Question8 = Data[11];
                    InfoDiag.Question9 = Data[12];
                    InfoDiag.Question10 = Data[13];
                    InfoDiag.Aurevoir = Data[14];

                    

                    InfoDiag.Dialogue1 = Data[15];
                    InfoDiag.Dialogue2 = Data[16];
                    InfoDiag.Dialogue3 = Data[17];
                    InfoDiag.Dialogue4 = Data[18];
                    InfoDiag.Dialogue5 = Data[19];
                    InfoDiag.Dialogue6 = Data[20];
                    InfoDiag.Dialogue7 = Data[21];
                    InfoDiag.Dialogue8 = Data[22];
                    InfoDiag.Dialogue9 = Data[23];    
                    InfoDiag.Dialogue10 = Data[24];    
                    InfoDiag.Dialogue11 = Data[25];    
                    InfoDiag.Dialogue12 = Data[26];    
                    InfoDiag.Dialogue13 = Data[27];    
                    InfoDiag.Dialogue14 = Data[28];    
                    InfoDiag.Dialogue15 = Data[29];    
                    InfoDiag.Dialogue16 = Data[30];    
                    InfoDiag.Dialogue17 = Data[31];    
                    InfoDiag.Dialogue18 = Data[32];    
                    InfoDiag.Dialogue19 = Data[33];    
                    InfoDiag.Dialogue20 = Data[34];    


                    myDialogueAdhérent.Add(InfoDiag);
                }
            }
        }
    }
}
