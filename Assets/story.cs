using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class story : MonoBehaviour
{
    // Start is called before the first frame update
    public Button START;

    public void pressStart()
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
            SceneManager.LoadScene("1_old");
        }

        if (type == 2)
        {
            SceneManager.LoadScene("Old2");
        }

        if (type == 1)
        {
            SceneManager.LoadScene("2_Game");
            Fungus.Flowchart.BroadcastFungusMessage("future");
        }
        
       
    }
    
}