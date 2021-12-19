using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool rotate = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(0, 0, -20);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }

    }
}
