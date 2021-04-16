using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject target;

    public void Boom()
    {
        Destroy(target);
        Destroy(this.gameObject);
    }
}
