using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToTranslateObject : MonoBehaviour
{
    #region Properties

    public TextMeshProUGUI TextToTranslate => textToTranslate;

    public string TranslationKey => translationKey;

    #endregion

    #region UnityInspector

    [SerializeField] private TextMeshProUGUI textToTranslate;

    [SerializeField] private string translationKey;

    #endregion

    #region Behaviour

    void Start()
    {
        LangueManager.Instance.onLangageUpdated += Translation;
    }

    public void SetTranslationKey(string value, bool automaticTranslation = true)
    {
        translationKey = value;

        if(automaticTranslation)
            Translation();
    }

    public void Translation()
    {
        if(translationKey == null)
        {
            Debug.LogWarning("translation key is null");
            return;
        }
        string text = GetCorrectText();
        textToTranslate.text = text;
    }

    public string GetCorrectText()
    {
        string text = LangueManager.Instance.Translate(translationKey);
        Debug.Log(text);
        return text;
    }

    private void OnDestroy()
    {
        LangueManager.Instance.onLangageUpdated -= Translation;
    }

    #endregion
}
