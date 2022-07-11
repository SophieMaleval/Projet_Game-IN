using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgesSwitch : MonoBehaviour
{
    #region UnityInspector

    public GameObject sidePont;
    public GameObject sousPont;

    public bool crossed = false;

    #endregion

    #region Behaviour

    // Start is called before the first frame update
    void Start()
    {
        sidePont = GameObject.FindGameObjectWithTag("SidePont");
        sousPont = GameObject.FindGameObjectWithTag("SousPont");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        crossed = !crossed;
    }
    // Update is called once per frame
    void Update()
    {
        ChangeCollider();
    }

    void ChangeCollider()
    {
        if (crossed == true)
        {
            sidePont.SetActive(true);
            sousPont.SetActive(false);
        }
        if (crossed == false)
        {
            sousPont.SetActive(true);
            sidePont.SetActive(false);
        }
    }

    #endregion
}
