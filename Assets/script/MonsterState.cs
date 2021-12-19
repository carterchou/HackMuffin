using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{

    private NavMeshAgent Agent;           
    public float walkSpeed = 0.5f;
    private float diatanceToPlayer;


    enum moveType
    {
        tooClose,
        chase,
        walk
    }

    moveType type = moveType.walk;

    Vector3 notarget;
    Transform target;


    void Start(){
        //NMA = GetComponent<NavMeshAgent>();
       
       
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

    }

    void Action(){
        

        if (GameManager.GetInstance().GetRoot()) {
            target = GameManager.GetInstance().GetRoot().transform;
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= 0.5f)
            {
                type = moveType.tooClose;
            } else if (distance <= 10) {
                type = moveType.chase;
            }
            else
            {
                notarget = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
                type = moveType.walk;
            }

        }
        else
        {
            notarget = transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10));
            type = moveType.walk;
        }

        switch (type) {
            case moveType.tooClose:
                Agent.SetDestination(transform.position);
                break;
            case moveType.chase:
                Agent.SetDestination(target.position);
                break;
            case moveType.walk:
                Agent.SetDestination(notarget);
                break;
        }

    }


    void Update() {
        Action();
    }

         
        
       
}
