using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMove : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    public bool isDeadBool;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PLAYER");
        isDeadBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeadBool) return;
        if (Vector3.Distance(transform.position, player.transform.position) > 3f)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    public void isDead()
    {
        isDeadBool = true;
        agent.isStopped = true;
    }
}
