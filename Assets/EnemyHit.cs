using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private EnemyManager emanager;
    void Start()
    {
        emanager = GetComponent<EnemyManager>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            float damageTaken = other.gameObject.GetComponent<bulletInfo>().damage;
            emanager.TakeDamage(damageTaken);
        }
    }
}
