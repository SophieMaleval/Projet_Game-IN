using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;


public class SceneManagerFeature : MonoBehaviour
{
    [SerializeField] private GameObject FadeImage ;
    public CinemachineVirtualCamera CMVirtualCam ;


    private void Awake() {
        if(GameObject.Find("Player") != null)
        {
            CMVirtualCam.Follow = GameObject.Find("Player").transform ;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().enabled = false ;
            other.gameObject.GetComponent<PlayerMovement>().ResetVelocity();
            GoCustom();
        }
    }

    public void GoCustom()
    {
        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        FadeImage.GetComponent<AnimationTransitionScene>().ShouldReveal = false ;
        yield return new WaitForSeconds(1.75f);
        SceneManager.LoadScene("Character Customer");
    }
}
