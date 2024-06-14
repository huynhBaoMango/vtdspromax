using UnityEngine;

public class Fireball : MonoBehaviour
{
    float damage;
    void Start()
    {
        damage = GameObject.FindAnyObjectByType<PlayerManager>().damage * 0.1f;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(damage);
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(damage);

        }
    }
}
