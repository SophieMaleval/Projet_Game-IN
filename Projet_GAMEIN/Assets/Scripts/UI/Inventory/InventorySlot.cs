using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AllosiusDev.TranslationSystem;

public class InventorySlot : MonoBehaviour
{
    #region Properties

    public RectTransform Border => border;
    public Image ObjectImage => objectImage;
    public RectTransform BackgroundNameObject => backgroundNameObject;
    public ToTranslateObject NameObject => nameObject;
    public Image Counter => counter;

    #endregion

    #region UnityInspector

    [SerializeField] private RectTransform border;
    [SerializeField] private Image objectImage;
    [SerializeField] private RectTransform backgroundNameObject;
    [SerializeField] private ToTranslateObject nameObject;
    [SerializeField] private Image counter;

    #endregion
}
