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
    private int currentHP;
    private AnimationsController animationsController;

    void Start()
    {
        // Thiết lập máu hiện tại bằng máu tối đa khi bắt đầu
        currentHP = maxHP;
        animationsController = GetComponent<AnimationsController>();
    }

    // Hàm này sẽ gọi khi enemy nhận sát thương
    public void TakeDamage(int amount)
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

    // Hàm này có thể gọi để gây sát thương lên người chơi
    public void AttackPlayer(Player player)
    {
        player.TakeDamage(damage);
    }

    // Hàm để xác định khi nào enemy tấn công người chơi (ví dụ, khi vào vùng va chạm)
    void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            AttackPlayer(player);
        }
    }
}
