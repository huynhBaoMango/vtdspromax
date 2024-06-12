﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Thêm thư viện UI
using TMPro;

public class levelManager : MonoBehaviour
{
    private PlayerManager playerManager;

    private int experience = 0;
    private int level = 1;
    public Slider xpSlider;
    public int maxXP = 1000;
    public Text levelText;
    public GameObject levelUpPanel;

    public TMP_Text[] buffNameTMPs = new TMP_Text[3];
    public TMP_Text[] buffDescriptionTMPs = new TMP_Text[3];
    public Button[] selectButtons = new Button[3];

    private List<string> buffNames = new List<string> { "HP", "Damage", "Speed", "CritRate", "CritDamage", "fireReset", "fireRate", "maxBulletCount", "reloadSpeed" };
    private string selectedAttribute;


    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        xpSlider.maxValue = maxXP;
        xpSlider.value = experience;
        UpdateLevelText();
        levelUpPanel.SetActive(false);
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        Debug.Log("Player gained " + amount + " XP. Total XP: " + experience);
        xpSlider.value = experience; // Cập nhật giá trị của Slider
        CheckLevelUp();
    }

    // Kiểm tra và tăng cấp nếu đủ XP
    void CheckLevelUp()
    {
        if (experience >= maxXP)
        {
            experience -= maxXP;
            xpSlider.value = experience;
            LevelUp();
            
        }
    }

    // Hàm xử lý việc tăng cấp
    void LevelUp()
    {
        playerManager.IncreaseCurrentHP(playerManager.maxHP/4);
        level++;
        maxXP = level * 100;
        xpSlider.maxValue = maxXP;
        UpdateLevelText(); // Cập nhật hiển thị cấp độ
        // Hiển thị Panel lựa chọn chỉ số
        levelUpPanel.SetActive(true);
        // Tạm dừng game
        Time.timeScale = 0f;
        // Khởi tạo các lựa chọn buff ngẫu nhiên
        InitializeLevelUpChoices();
    }

    // Cập nhật hiển thị cấp độ
    void UpdateLevelText()
    {
        levelText.text = "Level: " + level;
    }

    void InitializeLevelUpChoices()
    {
        // Lấy danh sách các buff hợp lệ
        List<string> validBuffs = playerManager.GetValidBuffs();

        // Tạo danh sách ngẫu nhiên từ các buff hợp lệ
        List<int> randomIndexes = new List<int>();
        while (randomIndexes.Count < 3 && validBuffs.Count > 0)
        {
            int randIndex = Random.Range(0, validBuffs.Count);
            if (!randomIndexes.Contains(randIndex))
            {
                randomIndexes.Add(randIndex);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i < randomIndexes.Count)
            {
                int buffIndex = randomIndexes[i];
                string buffName = validBuffs[buffIndex];
                buffNameTMPs[i].text = buffName;
                buffDescriptionTMPs[i].text = GetBuffDescription(buffName);
                selectButtons[i].onClick.RemoveAllListeners(); // Xóa tất cả các listener cũ
                selectButtons[i].onClick.AddListener(() => SelectBuff(buffName));
            }
            else
            {
                buffNameTMPs[i].text = "";
                buffDescriptionTMPs[i].text = "";
                selectButtons[i].onClick.RemoveAllListeners();
            }
        }
    }

    string GetBuffDescription(string buffName)
    {
        switch (buffName)
        {
            case "HP":
                return "Tăng máu thêm 20.";
            case "Damage":
                return "Tăng Damage thêm 10 phần trăm.";
            case "Speed":
                return "Tăng tốc độ di chuyển thêm 5 phần trăm.";
            case "CritRate":
                return "Tăng tỉ lệ chí mạng thêm 5 phần trăm";
            case "CritDamage":
                return "Tăng sát thương chí mạng thêm 50 phần trăm.";
            case "fireReset":
                return "Tăng tốc độ bắn thêm 10 phần trăm.";
            case "MaxBulletCount":
                return "Tăng thêm 10 viên đạn mỗi băng.";
            case "fireRate":
                return "Tăng liên xạ thêm 1, giảm 30 phần trăm damage";
            case "reloadSpeed":
                    return "Tăng tốc độ nạp đạn 10 phần trăm";
            default:
                return "";
        }
    }

    void SelectBuff(string buffName)
    {
        selectedAttribute = buffName;
        ConfirmSelection();
    }

    void ConfirmSelection()
    {
        // Cập nhật chỉ số dựa trên lựa chọn
        switch (selectedAttribute)
        {
            case "HP":
                playerManager.IncreaseHP(20);
                break;
            case "Damage":
                playerManager.IncreaseDamage(playerManager.damage * 0.1f);
                break;
            case "Speed":
                playerManager.IncreaseSpeed(playerManager.speed * 0.05f);
                break;
            case "CritRate":
                playerManager.IncreaseCritRate(0.05f);
                break;
            case "CritDamage":
                playerManager.IncreaseCritDamage(0.5f);
                break;
            case "fireReset":
                playerManager.IncreaseFireReset(playerManager.fireReset * 0.1f);
                break;
            case "fireRate":
                playerManager.IncreaseFireRate(1);
                break;
            case "MaxBulletCount":
                playerManager.IncreaseMaxBulletCount(10);
                break;
            case "reloadSpeed":
                playerManager.IncreaseReloadSpeed(playerManager.reloadSpeed*0.1f);
                break;
            default:
                break;
        }
        // Tăng số lần buff được chọn
        playerManager.BuffSelected(selectedAttribute);

        // Ẩn Panel và tiếp tục game
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Attribute increased: " + selectedAttribute);
        StartCoroutine(HideBuffTextAfterDelay(3f)); // Ẩn buffText sau 3 giây
    }

    IEnumerator HideBuffTextAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
    }

    public void EnemyDefeated(EnemyManager enemy)
    {
        GainExperience(enemy.experiencePoints);
    }


    float CalculateDamage()
    {
        // Tính toán sát thương, kiểm tra tỷ lệ chí mạng
        bool isCrit = Random.value < playerManager.critRate;
        if (isCrit)
        {
            return Mathf.RoundToInt(playerManager.damage * playerManager.critDamage);
        }
        return playerManager.damage;
    }
}
