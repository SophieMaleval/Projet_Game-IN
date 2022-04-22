using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameInPNJManager : MonoBehaviour
{
    [Header ("Animation Talk")]
    [SerializeField] private CinemachineVirtualCamera CineVCam; 
    private Transform Player ;
    [SerializeField] private Sprite ScooterPopUpImg ;

    [Header ("PNJ")]
    [SerializeField] private GameObject PNJLaurentHello ;
    [SerializeField] private GameObject PNJLaurent ;
    [SerializeField] private GameObject PNJManon ;
    [SerializeField] private GameObject PNJSophie ;

    private bool LaurentSayHello ;
    private bool PopUpScoot ;


    void Awake()
    {
        if(GameObject.Find("Player") != null) Player = GameObject.Find("Player").transform ;

        if(PlayerPrefs.GetInt("LaurentSayHello") == 0)    PlayerPrefs.SetInt("LaurentSayHello", 1);
        else    SwitchLaurentPNJ();

        StartCoroutine(CanSwitchShowPopUpScoot());
    }

    void SwitchLaurentPNJ()
    {
        GameObject LaurentPNJInstantiate = Instantiate(PNJLaurent, PNJLaurentHello.transform.position, Quaternion.identity);
        PNJLaurentHello.SetActive(false);
        CineVCam.Follow = Player ;
        GetComponent<BoxCollider2D>().enabled = false ;

        if(GameObject.Find("PopUp Displayer") != null && PopUpScoot) GameObject.Find("PopUp Displayer").GetComponent<PopUpManager>().CreatePopUpForScooter(ScooterPopUpImg);
    }
    void Update()
    {
        if(LaurentSayHello && !Player.GetComponent<PlayerScript>().InDiscussion && PNJLaurentHello.activeSelf) SwitchLaurentPNJ();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            PNJLaurentHello.GetComponent<PNJDialogue>().LunchDiscussion();
            CineVCam.Follow = PNJLaurentHello.transform ;
            LaurentSayHello = true ;
        }    
    }

    IEnumerator CanSwitchShowPopUpScoot()
    {
        yield return new WaitForSeconds(1f);
        PopUpScoot = true ;
    }

    
}
