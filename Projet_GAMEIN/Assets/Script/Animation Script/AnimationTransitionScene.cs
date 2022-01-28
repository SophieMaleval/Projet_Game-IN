using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AnimationTransitionScene : MonoBehaviour
{
    public Image ImageAnimate;

    [SerializeField] private float TransitionSpeed = 0.7f;

    public bool ShouldReveal;
    [SerializeField] private float CircleSize = 1.23f;


  
   


    void Awake()
    {
        ImageAnimate.material.SetFloat("_FloatResize", 0) ;
    //    ShouldReveal =  true;
    }



    void Update()
    {
  
        if(ShouldReveal)
        {
            ImageAnimate.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageAnimate.material.GetFloat("_FloatResize"), CircleSize, TransitionSpeed * Time.deltaTime));
        } else {
            ImageAnimate.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageAnimate.material.GetFloat("_FloatResize"), 0, TransitionSpeed * Time.deltaTime));  
        }
    }

    public void OpenningScene()
    {
        ImageAnimate.material.SetFloat("_FloatResize", 0) ;
        StartCoroutine(AnimOpeningScene()) ;
    }
    IEnumerator AnimOpeningScene()
    {
        yield return new WaitForSeconds(0.25f);
        ShouldReveal = true ;
        yield return new WaitForSeconds(2f) ;
        ImageAnimate.gameObject.SetActive(false);
    }

    public void QuitScene()
    {
        StopAllCoroutines();
        ShouldReveal = false ;
    }

 

}
