using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Các thuộc tính của enemy
    public int maxHP = 100;
    public int experiencePoints = 50;
    public int damage = 20;
    public float speed = 3f;
    public float attackRange = 3f;
    public float currentHP;
    private AnimationsController animationsController;
    private enemyMove emove;

    void Start()
    {
        currentHP = maxHP;
        animationsController = GetComponent<AnimationsController>();
        emove = GetComponent<enemyMove>();
    }

    // Hàm này sẽ gọi khi enemy nhận sát thương
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log("Enemy took " + amount + " damage. Current health: " + currentHP); // Debug để kiểm tra sát thương và máu còn lại của Enemy
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            animationsController.Hit(); // Phát animation khi Enemy nhận sát thương
        }
    }

    void Die()
    {
        emove.isDead();
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.EnemyDefeated(this);
        }
        animationsController.SetDead();
        Destroy(gameObject, 2f);
    }
}
