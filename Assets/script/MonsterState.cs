using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{

    private NavMeshAgent Agent;
    public GameObject player;             
    public float walkSpeed = 0.5f;
    private float diatanceToPlayer;
    //NavMeshAgent NMA;
    Vector3 notarget;
    Transform target;


    void Start(){
        //NMA = GetComponent<NavMeshAgent>();
        //player = monsterManager.GetInstance().player;

        notarget = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
        target = monsterManager.GetInstance().player.transform;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

    }

    //走路，若靠近玩家則會追趕

    void Action(){
        diatanceToPlayer = Vector2.Distance(player.transform.position, player.transform.position);
        //nowstate = true;
        if (diatanceToPlayer <= 30)
        {
            //NMA.destination = player.transform.position;
            Agent.SetDestination(target.position);
        }
        else
        {
            //Vector2 nextpos = new Vector2(transform.position.x, transform.position.y);
            //NMA.destination = nextpos;
            //notarget = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            Agent.SetDestination(notarget); 
        }
    }


    void Update() {
        Action();
    }

         
        
       
}
