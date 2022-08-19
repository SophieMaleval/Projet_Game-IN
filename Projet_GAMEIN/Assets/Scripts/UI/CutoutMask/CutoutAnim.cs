using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CutoutAnim : MonoBehaviour
{
    #region Properties

    public float CutoutAnimDuration => cutoutAnimDuration;

    #endregion

    #region UnityInspector

    [SerializeField] private RectTransform mask;

    [SerializeField] private float cutoutAnimDuration = 1f;

    [SerializeField] private Vector2 minMaskSize;
    [SerializeField] private Vector2 maxMaskSize;

    #endregion

    #region Behaviour

    private void Awake()
    {
        //FadeIn();
    }

    public void ResetMask()
    {
        Debug.Log("ResetMask");
        mask.GetComponent<Mask>().enabled = false;
        mask.GetComponent<Mask>().enabled = true;
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        ResetMask();
        mask.DOSizeDelta(minMaskSize, cutoutAnimDuration);
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        ResetMask();
        mask.DOSizeDelta(maxMaskSize, cutoutAnimDuration);
    }

    #endregion
}
