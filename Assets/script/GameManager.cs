using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public List<playController> slimes;
    public int playerMaxHP = 10;
    public int speed = 2;

    public string lasScene = ""; //lobby、前言 -1 mainMap 0 store 1
    public string nowScene = ""; //lobby、前言 -1 mainMap 0 store 1

    public bool isLose = false;
 
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

    public playController GetRoot() {
        foreach (playController slime in slimes)
        {
            if (slime.CheckRoot())
            {
                return slime;
            }
        }

        return null;
    }

    public void GoScene(string Name) {
        lasScene = nowScene;
        nowScene = Name;
        SceneManager.LoadScene(Name);
    }

}
