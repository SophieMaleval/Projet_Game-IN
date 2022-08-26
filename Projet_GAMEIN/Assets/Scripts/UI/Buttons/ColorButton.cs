using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    #region Properties

    public Image ContourCouleur => contourCouleur;

    #endregion

    #region UnityInspector

    [SerializeField] private Image contourCouleur;

    #endregion
}
