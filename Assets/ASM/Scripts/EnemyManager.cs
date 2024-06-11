using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Các thuộc tính của enemy
    public float maxHP;
    public int experiencePoints;
    public float damage;
    public float speed;
    public float attackRange;
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
        gameObject.tag = "Untagged";
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
