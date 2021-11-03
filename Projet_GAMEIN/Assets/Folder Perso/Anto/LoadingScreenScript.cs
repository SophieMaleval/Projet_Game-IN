using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndLoadMenu());
    }

    IEnumerator WaitAndLoadMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }
}
