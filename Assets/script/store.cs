using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class store : MonoBehaviour
{
    public void buy()
    {
        Fungus.Flowchart.BroadcastFungusMessage("buy");
    }
}
