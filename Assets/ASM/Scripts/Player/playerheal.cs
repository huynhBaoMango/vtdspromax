using UnityEngine;
using UnityEngine.UI;

public class Playerheal : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
           
        }
    }

     public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            die();
        }

        UpdateHealthBar();
    }
    public void Heal(int amount)
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
    }
}
