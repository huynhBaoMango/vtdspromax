<<<<<<< Updated upstream
﻿using UnityEngine;
=======
﻿using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
>>>>>>> Stashed changes
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Slider healthSlider;
    private PlayerManager pmanager;
    public GameObject deathCanvas;
    public TMP_Text yourScore;

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
<<<<<<< Updated upstream
        Destroy(gameObject);
        gameOverPanel.SetActive(true); // Hiển thị panel Game Over
=======
        deathCanvas.SetActive(true);
        yourScore.text = "Bạn đã sống sót đến đợt " + GameObject.Find("SPAWNER").GetComponent<Spawner>().wave;
>>>>>>> Stashed changes
        Time.timeScale = 0f;
    }
}
