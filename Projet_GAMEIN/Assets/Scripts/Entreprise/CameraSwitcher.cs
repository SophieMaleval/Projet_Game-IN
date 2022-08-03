using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    #region Properties

    static List<CinemachineVirtualCamera> Cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null ;

    #endregion

    #region Behaviour

    public static bool IsActiveCamera(CinemachineVirtualCamera Camera)
    {
        return Camera == ActiveCamera ;
    }

    public static void SwitchCamera(CinemachineVirtualCamera Camera)
    {
        Camera.Priority = 10 ;
        ActiveCamera = Camera ;

        foreach (CinemachineVirtualCamera C in Cameras)
        {
            if(C != Camera && C.Priority != 0)
            {
                C.Priority = 0 ;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera Camera)
    {
        Cameras.Add(Camera);
        //Debug.Log("Camera registered : " + Camera) ;
    }

    public static void Unregister(CinemachineVirtualCamera Camera)
    {
        Cameras.Remove(Camera);
        //Debug.Log("Camera unregistered: " + Camera) ;
    }

    #endregion
}
