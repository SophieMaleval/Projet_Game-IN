using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entreprise_ui_Name : MonoBehaviour
{
    #region Fields

    private TextMeshProUGUI NameTxt;
    private string NameValue;

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject buildingEntreprise;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Start()
    {
        NameTxt = this.GetComponent<TextMeshProUGUI>();
        NameValue = buildingEntreprise.name;
        NameTxt.text = NameValue.ToString();
    }

    #endregion
}
