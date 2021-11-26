using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset DialogData ;

    [System.Serializable]
    public class DialogueContainer
    {
        public string Entreprise ;
        public string Name ;        
        public string Langue ;
        public string Dialogue1 ;
        public string Dialogue2 ;
        public string Dialogue3 ;
        public string Dialogue4 ;
        public string Dialogue5 ;
        public string Dialogue6 ;
        public string Dialogue7 ;
        public string Dialogue8 ;
        public string Dialogue9 ;
    }


    public List<DialogueContainer> myDialogueAdhérent = new List<DialogueContainer>();



    void Start()
    {
       ReaderCSV();
    }

    void ReaderCSV() 
    {
        string[] LineData = DialogData.text.Split(new string[] { "§" , "\n" }, StringSplitOptions.None); // Data correspond à chaque Ligne



        for (int LD = 0; LD < LineData.Length; LD++)
        {
            if(LD != 0)
            {
                string[] Data = LineData[LD].Split(new string[] { ";" , "\n" }, StringSplitOptions.None); // Data correspond à chaque case    

                if(Data[0] != "")
                {
                    DialogueContainer InfoDiag = new DialogueContainer();
                    InfoDiag.Entreprise = Data[0];
                    InfoDiag.Name = Data[1];
                    InfoDiag.Langue = Data[2];
                    InfoDiag.Dialogue1 = Data[3];
                    InfoDiag.Dialogue2 = Data[4];
                    InfoDiag.Dialogue3 = Data[5];
                    InfoDiag.Dialogue4 = Data[6];
                    InfoDiag.Dialogue5 = Data[7];
                    InfoDiag.Dialogue6 = Data[8];
                    InfoDiag.Dialogue7 = Data[9];
                    InfoDiag.Dialogue8 = Data[10];
                    InfoDiag.Dialogue9 = Data[11];    


                    myDialogueAdhérent.Add(InfoDiag);
                }
            }


        }
    }
}
