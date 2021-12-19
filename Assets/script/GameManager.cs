using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public MainGame MainGameController;
    public int playerMaxHP = 10;
    public int speed = 2;

    public string lastScene = ""; //lobby、前言 -1 mainMap 0 store 1
    public string nowScene = ""; //lobby、前言 -1 mainMap 0 store 1

    public bool isLose = false;
 
    public static GameManager GetInstance() {
        if(instance == null)
        {
            instance = new GameObject("GameManager").AddComponent<GameManager>();
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    }

    private void Update()
    {
        if (isLose)
        {
            return;
        }

    }

    public playController GetRoot() {
        if(MainGameController == null)
        {
            return null;
        }
        foreach(playController slime in MainGameController.slimes)
        {
            if(slime == null)
            {
                continue;
            }
            if (slime.CheckRoot())
            {
                return slime;
            }
        }

        return null;
    }

    public void GoScene(string Name) {
        lastScene = nowScene;
        nowScene = Name;
        SceneManager.LoadScene(Name);
    }

}
