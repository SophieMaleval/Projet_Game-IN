using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AllosiusDev.DialogSystem;

public class Pnj_ui_Name : MonoBehaviour
{
    private TextMeshProUGUI NameTxt;
    private string NameValue;
    public NpcConversant Npc;
    // Start is called before the first frame update
    void Start()
    {
        NameTxt = this.GetComponent<TextMeshProUGUI>();
        NameValue = Npc.NamePnj;
        NameTxt.text = NameValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
