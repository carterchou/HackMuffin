using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Blood = 1000;
    public int attack = 100;
   


    // Update is called once per frame
    void Update()
    {
        if (Blood == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
