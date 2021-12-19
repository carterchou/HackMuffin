using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fungus.Flowchart.BroadcastFungusMessage("fail");
    }

}
