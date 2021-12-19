using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class openScene : MonoBehaviour
{
    public string sceneName;
    public string hint;
    public Text hintText;

    bool isHit = false;

    private void Update()
    {
        if (isHit)
        {
            if (hintText)
            {
                hintText.text = hint;
                hintText.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && string.IsNullOrEmpty(sceneName) == false)
            {
                GoScene();
            }
        }
        else
        {
            if (hintText)
            {
                hintText.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && other.collider.GetComponent<playController>().CheckRoot())
        {
            isHit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && other.collider.GetComponent<playController>().CheckRoot())
        {
            isHit = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<playController>().CheckRoot())
        {
            isHit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && other.GetComponent<playController>().CheckRoot())
        {
            isHit = false;
        }
    }

    public void GoScene() {
        GameManager.GetInstance().GoScene(sceneName);
    }

}
