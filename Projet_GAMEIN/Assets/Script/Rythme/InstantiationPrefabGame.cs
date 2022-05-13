using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InstantiationPrefabGame : MonoBehaviour
{
    public GameObject PrefabJeuRythme ;
    private GameObject PrefabInstant ;
    [HideInInspector] public PNJDialogue PNJScript ;
    public AudioSource AmbiantMusic ;
    public float VolumeAmbiantMusic ;


    // Start is called before the first frame update
    void Start()
    {
        VolumeAmbiantMusic = AmbiantMusic.volume ;
    }

    private void OnEnable() 
    {
        PrefabInstant =  Instantiate(PrefabJeuRythme, Vector3.zero, Quaternion.identity);
        PrefabInstant.GetComponentInChildren<RhythmManager>().PNJCurrent = PNJScript ;
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
        DOTween.To(x => AmbiantMusic.volume = x, AmbiantMusic.volume, 0, 0.5f) ;
    }

    void SetOnAmbiantMusic()
    {
        DOTween.To(x => AmbiantMusic.volume = x, 0, VolumeAmbiantMusic, 0.5f) ;
    }
    
}
