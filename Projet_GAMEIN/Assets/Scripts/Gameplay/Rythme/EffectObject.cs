using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectObject : MonoBehaviour
{
    #region UnityInspector

    public float lifeTime = 1f;

    #endregion

    #region Behaviour
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }

    #endregion
}
