using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class story : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fungus.Flowchart.BroadcastFungusMessage("gameStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStory(int type)
    {
        //古代場景
        //SceneManager.LoadScene(0);
        //int type = 0;
        flowChatManager.GetInstance().GetComponent<Fungus.Flowchart>().GetVariable("storyType");


        if (type == 1)
        {
            SceneManager.LoadScene(1);
            Fungus.Flowchart.BroadcastFungusMessage("future");
        }
        if (type == 2)
        {
            //正常遊戲模式
            Fungus.Flowchart.BroadcastFungusMessage("disappearend");
        }
       
    }
    
}