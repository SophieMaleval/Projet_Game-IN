using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    #region Behaviour

    void OnEnable() 
    {
        CameraSwitcher.Register(GetComponent<CinemachineVirtualCamera>());
    }

    void OnDisable() 
    {
        CameraSwitcher.Unregister(GetComponent<CinemachineVirtualCamera>());
    }

    #endregion
}
