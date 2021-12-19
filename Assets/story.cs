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

        
        flowChatManager.GetInstance().GetComponent<Fungus.Flowchart>().GetVariable("storyType");
        //古代場景
        if (type == 0)
        {
            SceneManager.LoadScene(0);
        }

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