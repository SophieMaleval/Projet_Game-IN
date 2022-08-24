using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterIconCtrl : MonoBehaviour
{
    #region Fields

    private Vector3 basePos;

    #endregion

    #region UnityInspector

    [SerializeField] public Vector3 moveUpOffset;
    [SerializeField] public Vector3 moveDownOffset;

    [SerializeField] public float moveDuration;

    [SerializeField] public float lifeDuration;

    [SerializeField] private SpriteRenderer sprite;

    #endregion

    #region Behaviour

    private void Awake()
    {
        basePos = transform.localPosition;
    }

    public void InitIcon(Sprite newSprite)
    {
        sprite.sprite = newSprite;
        SetIcon();
    }

    [ContextMenu("SetIcon")]
    public void SetIcon()
    {
        gameObject.SetActive(true);
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOLocalMove(moveUpOffset, moveDuration).OnComplete(MoveDown);
    }

    private void MoveDown()
    {
        transform.DOLocalMove(moveDownOffset, moveDuration).OnComplete(WaitBeforeDeactive);
    }

    private void WaitBeforeDeactive()
    {
        StartCoroutine(CoroutineLifeDuration());
    }

    private IEnumerator CoroutineLifeDuration()
    {
        yield return new WaitForSeconds(lifeDuration);

        gameObject.SetActive(false);
        transform.localPosition = basePos;
    }

    #endregion
}
