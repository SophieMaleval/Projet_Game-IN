using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineGetPlayer : MonoBehaviour
{
    #region Fields

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float baseCameraSize;

    private bool shake;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    #endregion

    #region UnityInspector

    [SerializeField] private float camShakeFrequencyGain = 2.56f;

    #endregion

    #region Behaviour

    private void Awake() 
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        baseCameraSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;

        if (GameManager.Instance.player != null)   // Récupère le player au lancement de la scène
        { 
            cinemachineVirtualCamera.Follow = GameManager.Instance.player.transform ;
        }

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = GetCinemachineMultiChannelPerlin();
        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = camShakeFrequencyGain;
        }
    }

    CinemachineBasicMultiChannelPerlin GetCinemachineMultiChannelPerlin()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        return cinemachineBasicMultiChannelPerlin;
    }

    public void FocusCamera(bool focusOn, float value)
    {
        if(focusOn)
        {
            baseCameraSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
            cinemachineVirtualCamera.m_Lens.OrthographicSize = value;
        }
        else
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = baseCameraSize;
        }
        
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = GetCinemachineMultiChannelPerlin();
        if (cinemachineBasicMultiChannelPerlin == null)
        {
            return;
        }

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        shake = true;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if(shake)
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;

                GetCinemachineMultiChannelPerlin().m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);

            }
            else
            {
                GetCinemachineMultiChannelPerlin().m_AmplitudeGain = 0;
                shake = false;
            }
        }
        
    }

    #endregion
}
