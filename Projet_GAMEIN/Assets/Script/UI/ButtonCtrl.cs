using AllosiusDev.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    #region Properties

    public Button button { get; protected set; }

    public AudioData SfxButtonHighlightedObject => sfxButtonHighlightedObject;
    public AudioData SfxButtonClickedObject => sfxButtonClickedObject;

    #endregion

    #region UnityInspector

    [SerializeField] private AudioData sfxButtonHighlightedObject;
    [SerializeField] private AudioData sfxButtonClickedObject;

    #endregion

    #region Behaviour

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        AudioController.Instance.PlayAudio(sfxButtonHighlightedObject);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down");
        AudioController.Instance.PlayAudio(sfxButtonClickedObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public virtual void Start()
    {

    }

    public void PlaySound(AudioData audioData)
    {
        AllosiusDev.Audio.AudioController.Instance.PlayAudio(audioData);
    }

    #endregion
}
