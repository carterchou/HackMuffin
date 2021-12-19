using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public GameObject weapon;
    public int Blood = 0;
    public int attack = 0;
    public int money = 0;
    private bool hurt = false;

    // Start is called before the first frame update
    void Start()
    {
        //HP.fillAmount; 
    }




    /*
    //е═жий╟кл
    public void CreateMonster()
    {
        int LittleMonsterNum = Random.Range(0, 5);
        int MediumMonsterNum = Random.Range(0, 4);
        int BigMonsterNum = Random.Range(0, 3);

        float x = Random.Range(-10, 10);

        Instantiate(Monster, new Vector3(x, 2.8f, 0), Quaternion.identity);

    }
    */

    // Update is called once per frame
    void Update() {

        hurt = false;
        Invoke("OnCollisionEnter2D", 1f);
        void OnCollisionEnter2D(Collider2D collision){
            if (collision.gameObject.tag == "weapon") {
                hurt = true;

            } else if (Blood == 0){
                Destroy(this.gameObject);
            }
        }
    }

}
