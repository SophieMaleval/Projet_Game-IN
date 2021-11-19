﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AnimationTransitionScene : MonoBehaviour
{
    public Image ImageComponnent;

    [SerializeField] private float TransitionSpeed = 0.7f;

    public bool ShouldReveal;
    [SerializeField] private float CircleSize = 1.23f;


    void Awake()
    {
        ImageComponnent.material.SetFloat("_FloatResize", 0) ;
        ShouldReveal =  true;
    }


    void Update()
    {
        if(ShouldReveal)
        {
            ImageComponnent.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageComponnent.material.GetFloat("_FloatResize"), CircleSize, TransitionSpeed * Time.deltaTime));
        } else {
            ImageComponnent.material.SetFloat("_FloatResize" , Mathf.MoveTowards(ImageComponnent.material.GetFloat("_FloatResize"), 0, TransitionSpeed * Time.deltaTime));
        }
    }
}