using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowChatManager : MonoBehaviour
{
    static flowChatManager instance;

    public static flowChatManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }
}
