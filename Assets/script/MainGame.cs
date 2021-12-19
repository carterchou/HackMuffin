using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();


        if (gameManager.lasScene == "3_Store")
        {
            gameManager.GetRoot().transform.localPosition = new Vector3(-0.95f, 2.14f, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isLose)
        {
            return;
        }

        
        if (gameManager.slimes.Count <= 0 && gameManager.isLose == false)
        {
            gameManager.isLose = true;
            Debug.Log("lose");
            return;
        }

    }
}
