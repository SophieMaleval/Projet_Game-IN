using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLight2DManager : MonoBehaviour
{
    private void Awake() {
        if(GameObject.Find("Global Light 2D") != null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
