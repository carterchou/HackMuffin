using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public List<playController> slimes;
    public int playerMaxHP = 10;
    public int speed = 2;

    bool isLose = false;
 
    public static GameManager GetInstance() {
        if(instance == null)
        {
            instance = new GameObject("GameManager").AddComponent<GameManager>();
            instance.slimes = new List<playController>();
        }

        return instance;
    }

    private void Update()
    {
        if (isLose)
        {
            return;
        }

        if(slimes.Count <= 0 && isLose == false)
        {
            isLose = true;
            Debug.Log("lose");
            return;
        }

        //checkRoot;
        bool foundRoot = false;
        foreach (playController slime in slimes)
        {
            if(foundRoot == false && slime.CheckRoot())
            {
                foundRoot = true;
                break;
            }
        }

        if(foundRoot == false)
        {
            slimes[0].SetRoot(true);
        }
    }
}
