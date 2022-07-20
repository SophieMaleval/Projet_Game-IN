using AllosiusDev.Audio;
using Core.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    #region Fields

    private PlayerMovement PM;
    private GameObject FadeImage ;

    #endregion

    #region UnityInspector

    //public string NameScene;
    [SerializeField] private SceneData sceneDataToLoad;

    [SerializeField] private AudioData sfxDoorOpening;
    
    [HideInInspector] public bool PlayerChangeScene = false ;

    #endregion

    #region Behaviour

    private void Awake() 
    {
        if(GameManager.Instance.player != null)
        {
            PM = GameManager.Instance.player.GetComponent<PlayerMovement>();
            FadeImage = GameManager.Instance.player.CanvasIndestrucitble.GetComponent<GameCanvasManager>().Fade;
        }
        else
        {
            Debug.LogWarning("Player is null");
        }
    }

    void Start() 
    {
        PM.EndActivity();
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerScript player = other.GetComponent<PlayerScript>();

        if(player != null && !PM.OnScooter)
        {
            Debug.Log("Player Change Scene");

            PM.StartActivity();
            PM.PlayerChangeScene = true ;
            PM.PlayerNeedInitialePosition = false ;
            if(other.gameObject.transform.position.y < (transform.position.y + GetComponent<BoxCollider2D>().offset.y)) PM.MakePlayerInGoodSens = true ;
            else PM.MakePlayerInGoodSens = false ;

            PM.ResetVelocity();

            //if(SceneManager.GetActiveScene().name == "Tilemaps test" || SceneManager.GetActiveScene().name == "Main") PM.GetComponent<PlayerScript>().MainSceneLoadPos = GiveNewPos();
            if(GameCore.Instance.CurrentScene.sceneOutside && GameCore.Instance.CurrentScene.isGameScene)
            {
                PM.GetComponent<PlayerScript>().MainSceneLoadPos = GiveNewPos();
            }
            //if(SceneManager.GetActiveScene().name == "Game In") PM.GetComponent<PlayerScript>().MainSceneLoadPos = new Vector2(-3.75f, -1.25f);
            if(GameCore.Instance.CurrentScene == SessionController.Instance.Game.StartLevelScene)
            {
                PM.GetComponent<PlayerScript>().MainSceneLoadPos = new Vector2(-3.75f, -1.25f);
            }
            //PM.GetComponent<PlayerScript>().PreviousSceneName = SceneManager.GetActiveScene().name;
            PM.GetComponent<PlayerScript>().PreviousSceneName = GameCore.Instance.CurrentScene;
            Debug.Log(PM.GetComponent<PlayerScript>().PreviousSceneName.name + " " + GameCore.Instance.CurrentScene.name);

            if (sfxDoorOpening != null)
            {
                AudioController.Instance.PlayAudio(sfxDoorOpening);
            }
            GoNewScene();
        }
    }

    Vector2 GiveNewPos()
    {
        BoxCollider2D BCol2D = GetComponent<BoxCollider2D>() ;
        Vector2 PositionSpawn = new Vector2(transform.position.x + BCol2D.offset.x, transform.localPosition.y + BCol2D.offset.y - 0.5f) ;
        return PositionSpawn ;
    }

    public void GoNewScene()
    {
        StartCoroutine(WaitBeforeChangeScene());
    }

    IEnumerator WaitBeforeChangeScene()
    {
        FadeImage.SetActive(true);
        FadeImage.GetComponent<AnimationTransitionScene>().QuitScene();
        yield return new WaitForSeconds(1f);
        AudioController.Instance.StopAllAmbients();
        AudioController.Instance.StopAllMusics();
        PlayerChangeScene = true ;
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene(NameScene);
        SceneLoader.Instance.ChangeScene(sceneDataToLoad);

    }

    #endregion

    #region Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GiveNewPos(), 0.1f);
    }

    #endregion
}
