using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagementController : MonoBehaviour
{
    public string SceneToLoad;
    public Image theImage;

    public float transitionSpeed = 0.7f;

    public bool shouldReveal;
    public float CircleSize = 1.23f;


    // Start is called before the first frame update
    void Start()
    {
        theImage =  GetComponent<Image>();
        shouldReveal =  true;
        

   
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(Mathf.MoveTowards(theImage.material.GetFloat("_FloatResize"), CircleSize, transitionSpeed * Time.deltaTime));


        if(shouldReveal){

            theImage.material.SetFloat("_FloatResize" , Mathf.MoveTowards(theImage.material.GetFloat("_FloatResize"), CircleSize, transitionSpeed * Time.deltaTime));
        }
        else
        {

            theImage.material.SetFloat("_FloatResize" , Mathf.MoveTowards(theImage.material.GetFloat("_FloatResize"), 0, transitionSpeed * Time.deltaTime));

            if(theImage.material.GetFloat("_FloatResize") ==  0)
            {

                SceneManager.LoadScene(SceneToLoad);
                shouldReveal = true;
            }


        }
        
    }


}
