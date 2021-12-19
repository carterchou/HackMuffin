
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterManager : MonoBehaviour
{
    static monsterManager instance;
    public static monsterManager GetInstance()
    {
        return instance;
    }

    //public GameObject player;
    public GameObject littleMonster;
    public GameObject mediumMonster;
    public GameObject bigMonster;
    public Transform CanvasRoot;

    public bool canGenerateMon = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //InvokeRepeating("GenerateMonster", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartGenMon();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StopGenMon();
        }
    }

    void GenerateMonster()
    {

        int randomMon = Random.Range(0, 3);
        Debug.Log(randomMon);
        if (randomMon == 0)
        {
            GameObject tempG = Instantiate(littleMonster);
            tempG.GetComponent<Transform>().localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            //tempG.GetComponent<Monster>().blood = 10000;

        }
        else if (randomMon == 1)
        {
            GameObject tempG = Instantiate(mediumMonster);
            tempG.GetComponent<Transform>().localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            //tempG.GetComponent<Monster>().blood = 10000;
        }
        else
        {
            GameObject tempG = Instantiate(bigMonster);
            tempG.GetComponent<Transform>().localPosition = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            //tempG.GetComponent<Monster>().blood = 10000;

        }

        if (canGenerateMon)
        {
            Invoke("GenerateMonster", 1f);
        }

    }


    public void StartGenMon()
    {
        canGenerateMon = true;
        Invoke("GenerateMonster", 1f);
    }

    public void StopGenMon()
    {
        canGenerateMon = false;
    }



}