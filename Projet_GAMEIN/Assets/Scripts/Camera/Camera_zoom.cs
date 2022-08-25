using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using AllosiusDev.DialogSystem;

public class Camera_zoom : MonoBehaviour
{
    internal bool grow;
    internal bool shrink;
    public bool HasTarget;

    [SerializeField] private GameObject Target;
    private float OldOrthographicSize;
    public float NewOrthographicSize;
    public float ZoomSpeed = 1;
    public CinemachineVirtualCamera vcam;

    public List<GameObject> objectsToSpawn = new List<GameObject>();

    public NpcConversant npcAssociated;


    void Start()
    {
        if(vcam == null)
        {
            vcam = GameCore.Instance.Vcam;
        }

        OldOrthographicSize = vcam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        if(grow)
        {
            if (vcam.m_Lens.OrthographicSize < NewOrthographicSize)
            {
                vcam.m_Lens.OrthographicSize += Time.deltaTime* ZoomSpeed;
            }
            else
            {
                grow = false;
            }
        }
        else if(shrink)
        {
            if (vcam.m_Lens.OrthographicSize > OldOrthographicSize)
            {
                vcam.m_Lens.OrthographicSize -= Time.deltaTime * ZoomSpeed;
            }
            else
            {
                shrink = false;
            }
        }
    }

    private void OnMouseOver()
    {
        Debug.Log("OnMouseOver");

        if (GameManager.Instance.zoomActive == false && npcAssociated != null)
        {
            npcAssociated.ActiveLocalCanvasObj();
        }
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.zoomActive == false && npcAssociated != null)
        {
            npcAssociated.DeactiveLocalCanvasObj();
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if(player != null)
        {
            if (GameCore.Instance != null && GameCore.Instance.dezoomTouchActive)
            {
                GameCore.Instance.Zoom(true);
            }

            GameManager.Instance.zoomActive = true;

            vcam.Priority = 11;
            shrink = false;
            grow = true;


            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                objectsToSpawn[i].SetActive(true);
            }

            if (HasTarget == true)
            {
                //vcam.Follow = null;
                vcam.Follow = Target.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
        if (player != null)
        {
            vcam.Priority = 7;
            grow = false;
            shrink = true;

            for (int i = 0; i < objectsToSpawn.Count; i++)
            {
                objectsToSpawn[i].SetActive(false);
            }

            if (HasTarget == true)
            {
                //vcam.transform.DOMove(new Vector3(GameManager.Instance.player.transform.position.x, GameManager.Instance.player.transform.transform.position.y, vcam.transform.position.z), 1);
                //vcam.Follow = GameManager.Instance.player.transform;
            }

            GameManager.Instance.zoomActive = false;
        }
            
    }
  
}
