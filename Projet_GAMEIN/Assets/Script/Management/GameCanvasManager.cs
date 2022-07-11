using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    #region Properties

    public GameObject Fade => fade;

    public InventoryScript inventory { get; set; }
    public QuestSys questManager { get; set; }

    #endregion

    #region UnityInspector

    [SerializeField] private GameObject fade;

    #endregion

    #region Behaviour

    private void Start()
    {
        GameManager.Instance.gameCanvasManager = this;
    }

    #endregion
}
