using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Các thuộc tính của enemy
    public float maxHP = 100;
    public int experiencePoints = 50;
    public float damage = 20;
    public float speed = 3f;
    public float attackRange = 3f;
    public float currentHP;
    private AnimationsController animationsController;
    private enemyMove emove;
    private bool isDie;

    void Start()
    {
        isDie = false;  
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
        levelManager player = FindObjectOfType<levelManager>();
        if (player != null && !isDie)
        {
            player.EnemyDefeated(this);
            isDie = true;
        }
        animationsController.SetDead();
        Destroy(gameObject, 2f);
    }
}
