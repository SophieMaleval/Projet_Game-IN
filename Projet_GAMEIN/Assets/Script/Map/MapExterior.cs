using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExterior : MonoBehaviour
{
    public PlayerMovement pM;
    public GameObject text;
    // Start is called before the first frame update
    void Awake()
    {
        pM = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    void OnEnable()
    {
        if (pM.InExterior == true)
        {
            text.SetActive(false);
        }
        else
        {
            text.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
