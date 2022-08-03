using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameInPNJManager : MonoBehaviour
{
    #region Fields

    private Transform Player ;

    private bool LaurentSayHello;
    private bool PopUpScoot;

    #endregion

    #region UnityInspector

    [Header ("Animation Talk")]
    [SerializeField] private CinemachineVirtualCamera CineVCam; 

    [SerializeField] private Sprite ScooterPopUpImg ;

    [Header ("PNJ")]
    [SerializeField] private GameObject PNJLaurentHello ;
    [SerializeField] private GameObject PNJLaurent ;
    [SerializeField] private GameObject PNJManon ;
    [SerializeField] private GameObject PNJSophie ;

    #endregion

    #region Behaviour

    void Awake()
    {
        if (GameManager.Instance.player != null)
        {
            Player = GameManager.Instance.player.transform;
        }
        else
        {
            Debug.LogWarning("Player is null");
        }

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

        /*if (GameManager.Instance.gameCanvasManager.inventory.PopUpManager != null && PopUpScoot)
        {
            GameManager.Instance.gameCanvasManager.inventory.PopUpManager.CreatePopUpForScooter(ScooterPopUpImg);
        }*/
    }
    void Update()
    {
        if(LaurentSayHello && !Player.GetComponent<PlayerScript>().InDiscussion && PNJLaurentHello.activeSelf) SwitchLaurentPNJ();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if(player != null)
        {
           
            PNJLaurentHello.GetComponent<PNJDialogue>().LunchDiscussion();
            PNJLaurentHello.GetComponent<BoxCollider2D>().enabled = true ;
            CineVCam.Follow = PNJLaurentHello.transform ;
            LaurentSayHello = true ;
        }    
    }

    IEnumerator CanSwitchShowPopUpScoot()
    {
        yield return new WaitForSeconds(1f);
        PopUpScoot = true ;
    }

    #endregion
}
