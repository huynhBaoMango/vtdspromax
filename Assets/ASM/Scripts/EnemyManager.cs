using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Các thuộc tính của enemy
    public int maxHP = 100;
    public int experiencePoints = 50;
    public int damage = 20;
    public float speed = 3f;
    public float currentHP;
    private AnimationsController animationsController;

    void Start()
    {
        // Thiết lập máu hiện tại bằng máu tối đa khi bắt đầu
        currentHP = maxHP;
        animationsController = GetComponent<AnimationsController>();
    }

    // Hàm này sẽ gọi khi enemy nhận sát thương
    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            animationsController.Hit();
        }
    }

    // Hàm này sẽ gọi khi enemy chết
    void Die()
    {
        // Thêm logic cho cái chết của enemy (vd: phát animation, thêm XP cho người chơi, etc.)
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.EnemyDefeated(this);
        }
        Destroy(gameObject);
    }
}
