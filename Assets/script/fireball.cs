using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{

    public Vector3 targetPos;
    public float speed = 0.1f;
    public bool isInit = false;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit == false)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        transform.Translate((targetPos - transform.position).normalized * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag != "Player")
        {
            Destroy(gameObject);
        }

    }
}
