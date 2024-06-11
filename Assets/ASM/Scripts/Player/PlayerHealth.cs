using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;
    private PlayerManager pmanager;

    public GameObject gameOverPanel; // Reference to the Game Over Panel

    void Start()
    {
        pmanager = GetComponent<PlayerManager>();
        maxHealth = pmanager.maxHP;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            die();
        }

        UpdateHealthBar();
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        healthSlider.value = currentHealth;
    }
    void die()
    {
        Destroy(gameObject);
        gameOverPanel.SetActive(true); // Hiển thị panel Game Over
        Time.timeScale = 0f;
    }
}
