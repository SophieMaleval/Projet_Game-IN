using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationPrefabGame : MonoBehaviour
{
    public GameObject PrefabJeuRythme ;
    private GameObject PrefabInstant ;
    [HideInInspector] public PNJDialogue PNJScript ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        PrefabInstant =  Instantiate(PrefabJeuRythme, Vector3.zero, Quaternion.identity);
        PrefabInstant.GetComponentInChildren<RhythmManager>().PNJCurrent = PNJScript ;
        PrefabInstant.GetComponentInChildren<RhythmManager>().DadMaster = this.gameObject ;
        PrefabInstant.transform.SetParent(this.transform);
    }

    void OnDisable()
    {
        Destroy(PrefabInstant);
    }
    
}
