using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    public static aim instance;
    public bool click = false;

    private void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (click)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector2(pos.x, pos.y);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        //transform.localPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Vector2 loca = Input.mousePosition;
        //Instantiate(mouse, locaation);

    }
}
