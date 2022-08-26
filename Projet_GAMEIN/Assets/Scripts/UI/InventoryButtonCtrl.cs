using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonCtrl : ButtonCtrl
{
    #region UnityInspector

    [SerializeField] private Sprite spriteUnlocked;
    [SerializeField] private Sprite spriteLocked;

    [SerializeField] private ChildrenImage[] childrenImages;

    #endregion

    #region Structs

    [System.Serializable]
    public struct ChildrenImage
    {
        [SerializeField] public Image childrenImg;
        [SerializeField] public Sprite childrenSpriteUnlocked;
        [SerializeField] public Sprite childrenSpriteLocked;
    }

    #endregion

    #region Behaviour

    public void SetLockedState(bool value)
    {
        button.enabled = !value;

        if (value)
        {
            image.sprite = spriteLocked;

            for (int i = 0; i < childrenImages.Length; i++)
            {
                childrenImages[i].childrenImg.sprite = childrenImages[i].childrenSpriteLocked;
            }
        }
        else
        {
            image.sprite = spriteUnlocked;

            for (int i = 0; i < childrenImages.Length; i++)
            {
                childrenImages[i].childrenImg.sprite = childrenImages[i].childrenSpriteUnlocked;
            }
        }
    }

    #endregion
}
