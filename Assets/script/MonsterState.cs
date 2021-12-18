using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    
    public GameObject player;             
    private bool nowstate = true;
    public float walkSpeed = 0.5f;
    private float diatanceToPlayer;
    NavMeshAgent NMA;

    void Start(){
        NMA = GetComponent<NavMeshAgent>();
        player = GameManager.GetInstance().player;

    }

    //走路，若靠近玩家則會追趕

    void Action(){
        diatanceToPlayer = Vector2.Distance(player.transform.position, player.transform.position);
        nowstate = true;
        if (diatanceToPlayer <= 30)
        {
            NMA.destination = player.transform.position;
        }
        else
        {
            Vector2 nextpos = new Vector2(transform.position.x, transform.position.y);
            NMA.destination = nextpos;
        }
    }


    void Update() {
        Action();
    }

         
        
       
}
