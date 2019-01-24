using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public float Time = 2f;
    void Start()
    {
        Destroy(gameObject, Time);
    }
}
