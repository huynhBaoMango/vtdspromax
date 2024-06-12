using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    private PlayerManager pmanager;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
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
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth; // Cập nhật thanh máu
        if(currentHealth > 0) {
            FindAnyObjectByType<AudioManager>().Play("Hurt");
            FindAnyObjectByType<AudioManager>().Play("Hurt1");
        }
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (!pmanager.isDead) { FindAnyObjectByType<AudioManager>().PlayButWait("Dead"); }
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
        pmanager.isDead = true;
        anim.SetTrigger("isdead");
        
        if (PlayerPrefs.GetInt("highScore") != null)
        {
            int oldScore = PlayerPrefs.GetInt("highScore");
            if (oldScore < GameObject.Find("SPAWNER").GetComponent<Spawner>().wave)
            {
                PlayerPrefs.SetInt("highScore", GameObject.Find("SPAWNER").GetComponent<Spawner>().wave);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", GetComponent<Spawner>().wave);
        }

    }
}