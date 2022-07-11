using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerButton : MonoBehaviour
{
    #region Properties

    public Image Icon => icon;

    #endregion

    #region UnityInspector

    [SerializeField] private Image icon;

    #endregion
}
