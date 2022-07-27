using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AnimationTransitionScene : MonoBehaviour
{
    #region UnityInspector

    public Image ImageAnimate;

    [SerializeField] private float TransitionSpeed = 0.7f;

    public bool ShouldReveal;
    [SerializeField] private float CircleSize = 1.23f;

    #endregion

    #region Behaviour

    void Awake()
    {
        ImageAnimate.material.SetFloat("_FloatResize", 0) ;
        Debug.LogWarning(ImageAnimate.material.name);
    //    ShouldReveal =  true;
    }

    void Update()
    {
        Debug.Log(ImageAnimate.material.shader.GetPropertyCount());

        if (ShouldReveal)
        {
            Debug.LogWarning("Should Reveal");
            ImageAnimate.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageAnimate.material.GetFloat("_FloatResize"), CircleSize, TransitionSpeed * Time.deltaTime));
        } else {
            Debug.LogWarning("Not Should Reveal");
            ImageAnimate.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageAnimate.material.GetFloat("_FloatResize"), 0, TransitionSpeed * Time.deltaTime));  
        }
    }

    public void OpenningScene()
    {
        ImageAnimate.material.SetFloat("_FloatResize", 0) ;
        Debug.LogWarning(ImageAnimate.material.name);
        /*StartCoroutine(AnimOpeningScene()) ;*/

        ShouldReveal = true;
    }
    /*IEnumerator AnimOpeningScene()
    {
        Debug.LogWarning("Start Anim Opening Scene");
        yield return new WaitForSeconds(0.25f);
        ShouldReveal = true ;
        yield return new WaitForSeconds(2f) ;
        ImageAnimate.gameObject.SetActive(false);
        Debug.LogWarning("End Anim Opening Scene");
    }*/

    public void QuitScene()
    {
        //StopAllCoroutines();
        ShouldReveal = false ;
    }

    #endregion

}
