using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int Blood = 1000;
    public int attack = 100;


    private void OnCollisionEnter2D(Collision2D other)
    {
	    if (other.collider.tag == "Player" && other.collider.GetComponent<playController>().CheckRoot())
        {
            SceneManager.LoadScene("4_end");
        }
    }
}
