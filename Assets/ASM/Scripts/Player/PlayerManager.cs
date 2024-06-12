    using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    public class PlayerManager : MonoBehaviour
    {
        public float maxHP = 100;
        public float speed = 5f;
        public float damage = 10;
        public float critRate = 0.1f; // Tỷ lệ chí mạng, giá trị từ 0 đến 1
        public float critDamage = 1.3f; // Sát thương chí mạng, ví dụ: 2.0f tức là gấp đôi sát thương thường
        public int maxBulletCount = 30; 
        public int currentBulletCount = 30;
        public float reloadSpeed = 4f;
        public float fireReset = 4f;
        public float fireRate = 1f;
        public bool isDead = false;

        private PlayerHealth playerHealth;

        // Dictionary để theo dõi số lần mỗi buff được chọn
        [HideInInspector] public Dictionary<string, int> buffSelectionCounts = new Dictionary<string, int>
        {
            { "HP", 0 },
            { "Damage", 0 },
            { "Speed", 0 },
            { "CritRate", 0 },
            { "CritDamage", 0 },
            { "MaxBulletCount", 0 },
            { "fireReset", 0 },
            { "fireRate", 0 },
            { "reloadSpeed", 0 }
        };

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Hàm để tăng các chỉ số
    public void IncreaseHP(float amount)
        {
            maxHP += amount;
            playerHealth.currentHealth += amount;
        }

        public void IncreaseCurrentHP(float amount)
        {
        playerHealth.currentHealth += amount;
        }

        public void IncreaseSpeed(float amount)
        {
            speed += amount;
        }

        public void IncreaseDamage(float amount)
        {
            damage += amount;
        }

        public void IncreaseCritRate(float amount)
        {
            critRate += amount;
        }

        public void IncreaseCritDamage(float amount)
        {
            critDamage += amount;
        }

        public void IncreaseMaxBulletCount(int amount)
        {
            maxBulletCount += amount;
        }
        public void IncreaseCurrentBulletCount(int amount)
        {
            currentBulletCount += amount;
            currentBulletCount = Mathf.Clamp(currentBulletCount, 0, maxBulletCount);
        }
        public void DecreaseCurrentBulletCount(int amount)
        {
            currentBulletCount -= amount;
            
            currentBulletCount = Mathf.Max(0, currentBulletCount);
        }

        public void IncreaseFireReset(float amount)
        {
            fireReset -= amount;
        }

        public void IncreaseFireRate(int amount)
        {
            fireRate += amount;
            damage -= damage * 0.4f;
        }
    public void IncreaseReloadSpeed(float amount)
    {
        reloadSpeed -= amount;
    }

    // Hàm để tăng số lần buff được chọn
    public bool CanSelectBuff(string buffName)
        {
            return buffSelectionCounts[buffName] < 10;
        }

        public void BuffSelected(string buffName)
        {
            if (buffSelectionCounts.ContainsKey(buffName))
            {
                buffSelectionCounts[buffName]++;
            }
        }

        // Hàm để kiểm tra nếu có đủ buff hợp lệ để chọn
        public List<string> GetValidBuffs()
        {
            List<string> validBuffs = new List<string>();
            foreach (var buff in buffSelectionCounts)
            {
                if (buff.Value < 10)
                {
                    validBuffs.Add(buff.Key);
                }
            }
            return validBuffs;
        }
    }
