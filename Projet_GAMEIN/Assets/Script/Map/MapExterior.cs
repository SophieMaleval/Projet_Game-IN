using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExterior : MonoBehaviour
{
    #region UnityInspector

    public PlayerMovement pM;
    public GameObject text;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Awake()
    {
        pM = GameManager.Instance.player.GetComponent<PlayerMovement>();
    }
    void OnEnable()
    {
        if (pM.InExterior == true)
        {
            text.SetActive(false);
        }
        else
        {
            text.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
