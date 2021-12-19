using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{

    public Text price;
    public GameObject[] star;
    public GameObject isWeapon;
    public GameObject isShield;
    public GameObject[] weapon;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        SetUI(weapon[index].GetComponent<itemObj>());
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= 6)
            index = 6;

        if (index < 0)
            index = 0;



        if (index == 0)
        {
            weapon[0].gameObject.SetActive(true);
        }
        
    }
    public void Next()
    {
        index ++;

        if(index> weapon.Length - 1)
        {
            index = 0;
        }

        SetUI(weapon[index].GetComponent<itemObj>());

        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].gameObject.SetActive(i == index);
        }
        Debug.Log(index);
    }

    public void Previous()
        {
        index--;

        if (index < 0)
        {
            index = weapon.Length - 1;
        }

        SetUI(weapon[index].GetComponent<itemObj>());

        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].gameObject.SetActive(i == index);
        }
    }

    void SetUI(itemObj item)
    {
        price.text = item.price.ToString();
        for (int i = 0; i < star.Length; i++)
        {
            star[i].SetActive(i + 1 <= item.star);
        }
        isWeapon.SetActive(item.weapon);
        isShield.SetActive(!item.weapon);
    }
}