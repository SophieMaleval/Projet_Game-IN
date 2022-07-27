using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AllosiusDev.Audio;
using AllosiusDev.DialogSystem;

public class InstantiationPrefabGame : MonoBehaviour
{
    #region Fields

    private GameObject PrefabInstant ;

    #endregion

    #region Properties

    public PNJDialogue PNJScript { get; set; }
    public NpcConversant npcScript { get; set; }

    #endregion

    #region UnityInspector

    public GameObject PrefabJeuRythme ;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.StopAudio(GameCore.Instance.MainMusic);
    }

    private void OnEnable() 
    {
        PrefabInstant =  Instantiate(PrefabJeuRythme, Vector3.zero, Quaternion.identity);
        if(PNJScript != null)
            PrefabInstant.GetComponentInChildren<RhythmManager>().PNJCurrent = PNJScript ;
        if(npcScript != null)
            PrefabInstant.GetComponentInChildren<RhythmManager>().npcCurrent = npcScript ;
        PrefabInstant.GetComponentInChildren<RhythmManager>().DadMaster = this.gameObject ;
        PrefabInstant.transform.SetParent(this.transform);

        SetOffAmbiantMusic();
    }

    void OnDisable()
    {
        SetOnAmbiantMusic();
        Destroy(PrefabInstant);
    }


    void SetOffAmbiantMusic()
    {
        //DOTween.To(x => AmbiantMusic.volume = x, AmbiantMusic.volume, 0, 0.5f) ;
        AudioController.Instance.StopAudio(GameCore.Instance.MainMusic);
    }

    void SetOnAmbiantMusic()
    {
        //DOTween.To(x => AmbiantMusic.volume = x, 0, VolumeAmbiantMusic, 0.5f) ;
        AudioController.Instance.PlayAudio(GameCore.Instance.MainMusic);
    }

    #endregion

}
