using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float damage = 0f;

    void Start()
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();
        if (player != null)
        {
            damage = player.damage * 0.5f;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
