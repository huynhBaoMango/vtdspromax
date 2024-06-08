using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHP = 100;
    public float speed = 5f;
    public int damage = 10;
    public float critRate = 0.1f; // Tỷ lệ chí mạng, giá trị từ 0 đến 1
    public float critDamage = 2.0f; // Sát thương chí mạng, ví dụ: 2.0f tức là gấp đôi sát thương thường

    // Dictionary để theo dõi số lần mỗi buff được chọn
    private Dictionary<string, int> buffSelectionCounts = new Dictionary<string, int>
    {
        { "HP", 0 },
        { "Damage", 0 },
        { "Speed", 0 },
        { "CritRate", 0 },
        { "CritDamage", 0 }
    };

    // Hàm để tăng các chỉ số
    public void IncreaseHP(int amount)
    {
        maxHP += amount;
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }

    public void IncreaseDamage(int amount)
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

    // Hàm để tăng số lần buff được chọn
    public bool CanSelectBuff(string buffName)
    {
        return buffSelectionCounts[buffName] < 7;
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
            if (buff.Value < 7)
            {
                validBuffs.Add(buff.Key);
            }
        }
        return validBuffs;
    }
}
