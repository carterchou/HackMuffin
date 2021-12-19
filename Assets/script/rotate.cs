using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool isRotate = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            transform.Rotate(0, 0, -20);
        }
        else
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

    }
}
