using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainGame : MonoBehaviour
{
    GameManager gameManager;
    public List<playController> slimes;
    public GameObject SlimePrefab;
    // Start is called before the first frame update
    bool isInit = false;

    public Image hpBar;
    public Text money;

    void Start()
    {
        gameManager = GameManager.GetInstance();

        gameManager.MainGameController = this;

        //create first
        playController root = Instantiate(SlimePrefab).GetComponent<playController>();
        root.init(GameManager.GetInstance().GameHp);
        root.SetRoot(true);

        if (gameManager.lastScene == "3_Store")
        {
            gameManager.GetRoot().transform.localPosition = new Vector3(-0.95f, 2.14f, 0);
        }
        else
        {
            gameManager.GetRoot().transform.localPosition = new Vector3(-2.58f, 0.31f, 0);
        }


        isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInit == false)
        {
            return;
        }

        if (gameManager.isLose)
        {
            return;
        }

        
        if (slimes.Count <= 0 && gameManager.isLose == false)
        {
            gameManager.isLose = true;
            Debug.Log("lose");
            return;
        }

        //checkRoot;
        bool foundRoot = false;
        float hp = 0;
        for (int i = slimes.Count - 1; i >= 0; i--)
        {
            if (slimes[i] == null)
            {
                slimes.RemoveAt(i);
                continue;
            }

            if (foundRoot == false && slimes[i].CheckRoot())
            {
                foundRoot = true;
            }

            //HP
            hp += slimes[i].hp;
        }

        if (foundRoot == false && slimes.Count > 0)
        {
            slimes[0].SetRoot(true);
        }

        hpBar.fillAmount = hp / gameManager.playerMaxHP;
        money.text = gameManager.GameMoney.ToString();

    }
}
