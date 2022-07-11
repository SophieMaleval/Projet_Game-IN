using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CheckMethod
{
    Distance, 
    Trigger
}
public class DynamicLoad : MonoBehaviour
{
    #region Fields

    //Scene state
    bool isLoaded; //eviter de charger 2x
    bool shouldLoad; //pour la méthode en trigger

    #endregion

    #region UnityInspector

    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    #endregion

    #region Behaviour

    private void Awake()
    {
        if (GameManager.Instance.player != null)
        {
            player = GameManager.Instance.player.transform;
        }
        else
        {
            Debug.LogWarning("Player is null");
        }

    }
    void Start()
    {

    }
    void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive); // le nom du game object "Part n+1" doit être identique à celui de la scène à charger
            isLoaded = true;
        }
    }

    void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (checkMethod == CheckMethod.Distance)
        {
            DistanceCheck();
        }
        else if (checkMethod == CheckMethod.Trigger)
        {
            TriggerCheck();
        }
    }
    //method Distance
    void DistanceCheck()
    {
        if (Vector3.Distance(player.position, transform.position) < loadRange)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }



    //method Trigger
    void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            shouldLoad = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();
        if (player != null)
        {
            shouldLoad = false;
        }
    }

    #endregion
}
