using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllosiusDev;

public class GameManager : Singleton<GameManager>
{
    #region Properties

    public PlayerScript player { get; set; }

    public GameCanvasManager gameCanvasManager { get; set; }

    #endregion

    #region Behaviour

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<PlayerScript>();
    }

    #endregion
}
