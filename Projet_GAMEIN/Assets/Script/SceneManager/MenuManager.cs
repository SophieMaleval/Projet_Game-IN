using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using DG.Tweening ;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
 public string NameScene ;
    public AnimationTransitionScene ATS;

    public GameObject OptionsPanel;
    public GameObject ToggleFrançais;
    public GameObject ToggleAnglais;

    [SerializeField] public GameObject FadeImage ;

    // Start is called before the first frame update
    void Start()
    {
     
        StartCoroutine(WaitTransitionAnim());
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetKeyDown(KeyCode.Space))
        {
            FadeImage.SetActive(true);
            
            StartCoroutine("Fade");
        
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

    public void OnClickEN()
    {
          Debug.Log("gg");

        ToggleFrançais.SetActive(false);
        ToggleAnglais.SetActive(true);
    }

     public void OnClickFR()
    {
        Debug.Log("ff");

        ToggleAnglais.SetActive(false);
        ToggleFrançais.SetActive(true);
    }


        IEnumerator WaitTransitionAnim()
    {
        yield return new WaitForSeconds(0.25f);
        FadeImage.GetComponent<AnimationTransitionScene>().enabled = true ;
        yield return new WaitForSeconds(2f) ;
        FadeImage.SetActive(false);

    }
}
