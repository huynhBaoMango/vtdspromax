using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthSlider;
    private PlayerManager pmanager;
    private Animator anim;
    public Image bloodtop;
    public Image bloodbot;
    public Image bloodleft;
    public Image bloodright;
    public float blinkDuration = 1f; // Thời gian chóp chóp
    public float blinkSpeed = 0.1f; // Tốc độ chóp chóp

    void Start()
    {
        bloodtop.enabled=false;
        bloodbot.enabled=false;
        bloodleft.enabled=false;
        bloodright.enabled=false;
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
        StartCoroutine(BlinkScreenDamage());
        currentHealth -= damage;
        healthSlider.value = currentHealth; // Cập nhật thanh máu
        if(currentHealth > 0) {
        bloodtop.enabled=true;
        bloodbot.enabled=true;
        bloodleft.enabled=true;
        bloodright.enabled=true;
        Invoke("HideScreenDamage", 0.5f);
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
    void HideScreenDamage()
    {
       bloodtop.enabled=false;
        bloodbot.enabled=false;
        bloodleft.enabled=false;
        bloodright.enabled=false;
    }
    IEnumerator BlinkScreenDamage()
    {
        float endTime = Time.time + blinkDuration;
        while (Time.time < endTime)
        {
            bloodtop.enabled = !bloodtop.enabled;
            bloodbot.enabled = !bloodbot.enabled;
            bloodleft.enabled = !bloodleft.enabled;
            bloodright.enabled = !bloodright.enabled; // Đảo trạng thái hiển thị của Image
            yield return new WaitForSeconds(blinkSpeed); // Đợi một khoảng thời gian trước khi chóp tiếp
        }
         bloodtop.enabled  = false; 
        bloodbot.enabled = false;
           bloodleft.enabled =false;
            bloodright.enabled =false;// Đảm bảo Image được tắt sau khi chóp xong
    }


}