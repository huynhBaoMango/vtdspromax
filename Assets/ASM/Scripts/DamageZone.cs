using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float duration = 5f;
    public float damagePerSecond = 10f;
    private float delayDamage;
    public float damage;

    void Start()
    {
        delayDamage = 0;
        Destroy(gameObject, 5f);
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            delayDamage -= Time.deltaTime;
            if(delayDamage <= 0)
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                delayDamage = 1f;
            }
        }
    }

}
