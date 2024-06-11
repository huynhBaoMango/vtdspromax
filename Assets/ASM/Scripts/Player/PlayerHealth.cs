using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    private PlayerManager pmanager;

    void Start()
    {
        pmanager = GetComponent<PlayerManager>();
        if (pmanager != null)
        {
            maxHealth = pmanager.maxHP; // Cập nhật maxHealth từ PlayerManager
            currentHealth = maxHealth;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("PlayerManager component not found on " + gameObject.name);
        }
    }

    private void Update()
    {
        maxHealth = pmanager.maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth; // Cập nhật thanh máu

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthSlider.value = currentHealth; // Cập nhật thanh máu
    }

    void Die()
    {
        // Optional: Add any additional logic for player death here.
        Destroy(gameObject);
        Time.timeScale = 0f; // Dừng game
    }
}
