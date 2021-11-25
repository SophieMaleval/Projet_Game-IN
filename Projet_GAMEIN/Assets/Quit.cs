using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{

    public string NameScene ;
    public AnimationTransitionScene ATS;

    public GameObject OptionsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine("Fade");
        ;
            Debug.Log("A key or mouse click has been detected");
        }
        
    }


    IEnumerator Fade() 
{
        ATS.ShouldReveal = false;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Character Customer");
      
}

 
    public void OnclickOption()
    {
        OptionsPanel.SetActive(true);
    }

    public void OnQuitclickOption()
    {
        OptionsPanel.SetActive(false);
    }
}
