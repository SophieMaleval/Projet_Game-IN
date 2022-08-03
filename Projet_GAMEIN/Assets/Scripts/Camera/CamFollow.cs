using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    #region UnityInspector

    public Transform player;

    #endregion

    #region Behaviour

    void Awake()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
    }

    #endregion
}
