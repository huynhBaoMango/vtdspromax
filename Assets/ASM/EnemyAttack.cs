using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private AnimationsController animationsController;
    private GameObject player;
    private EnemyManager emanager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER");
        animationsController = GetComponent<AnimationsController>();
        emanager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Vector3.Distance(transform.position, player.transform.position) < emanager.attackRange)
       {
            animationsController.Attack();
       }
    }

    public void AttackOnPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < emanager.attackRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(emanager.damage);
        }
    }
}
