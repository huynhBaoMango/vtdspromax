using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Thêm thư viện UI
using TMPro; // Thêm thư viện TextMeshPro

public class Player : MonoBehaviour
{
    public PlayerManager playerManager; // Tham chiếu đến PlayerManager

    private int currentHP;
    private int experience = 0;
    private int level = 1; // Cấp độ ban đầu của người chơi
    public Slider xpSlider; // Thêm biến tham chiếu đến Slider
    public int maxXP = 1000; // XP cần để lên cấp
    public TMP_Text levelText; // Biến tham chiếu đến TMP Text để hiển thị cấp độ
    public TMP_Text buffText; // Biến tham chiếu đến TMP Text để hiển thị nội dung buff

    public GameObject levelUpPanel; // Thêm biến tham chiếu đến Panel

    // Thêm các biến tham chiếu đến các thành phần trong các panel con
    public TMP_Text[] buffNameTMPs = new TMP_Text[3];
    public TMP_Text[] buffDescriptionTMPs = new TMP_Text[3];
    public Button[] selectButtons = new Button[3];

    private List<string> buffNames = new List<string> { "HP", "Damage", "Speed", "CritRate", "CritDamage" };
    private string selectedAttribute; // Biến để lưu lựa chọn của người chơi

    void Start()
    {
        currentHP = playerManager.maxHP;
        // Đặt giá trị tối đa của Slider là maxXP
        xpSlider.maxValue = maxXP;
        xpSlider.value = experience;
        // Cập nhật hiển thị cấp độ ban đầu
        UpdateLevelText();
        // Ẩn Panel lúc đầu
        levelUpPanel.SetActive(false);
        buffText.gameObject.SetActive(false); // Ẩn buffText lúc ban đầu
    }

    void Update()
    {
        // Kiểm tra nếu phím M được nhấn
        if (Input.GetKeyDown(KeyCode.M))
        {
            Attack();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Player took " + amount + " damage. Current HP: " + currentHP);
        }
    }

    void Die()
    {
        // Thêm logic cho cái chết của người chơi
        Debug.Log("Player died!");
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
            LevelUp();
            xpSlider.value = experience; // Đặt lại giá trị của Slider
        }
    }

    // Hàm xử lý việc tăng cấp
    void LevelUp()
    {
        level++;
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
                return "Increased HP by 20.";
            case "Damage":
                return "Increased Damage by 5.";
            case "Speed":
                return "Increased Speed by 1.";
            case "CritRate":
                return "Increased Crit Rate by 0.05.";
            case "CritDamage":
                return "Increased Crit Damage by 0.5.";
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
                playerManager.IncreaseDamage(5);
                break;
            case "Speed":
                playerManager.IncreaseSpeed(1f);
                break;
            case "CritRate":
                playerManager.IncreaseCritRate(0.05f);
                break;
            case "CritDamage":
                playerManager.IncreaseCritDamage(0.5f);
                break;
            default:
                break;
        }
        // Tăng số lần buff được chọn
        playerManager.BuffSelected(selectedAttribute);

        // Ẩn Panel và tiếp tục game
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        buffText.text = "Attribute increased: " + selectedAttribute;
        buffText.gameObject.SetActive(true); // Hiển thị buffText
        Debug.Log("Attribute increased: " + selectedAttribute);
        StartCoroutine(HideBuffTextAfterDelay(3f)); // Ẩn buffText sau 3 giây
    }

    IEnumerator HideBuffTextAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        buffText.gameObject.SetActive(false);
    }

    // Gọi hàm này khi enemy chết để thêm XP cho người chơi
    public void EnemyDefeated(Enemy enemy)
    {
        GainExperience(enemy.experiencePoints);
    }

    // Hàm tấn công enemy
    void Attack()
    {
        // Giả sử chỉ có một enemy cần tấn công
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy != null)
        {
            int damageDealt = CalculateDamage();
            enemy.TakeDamage(damageDealt);
            Debug.Log("Attacked enemy for " + damageDealt + " damage.");
        }
        else
        {
            Debug.Log("No enemy to attack.");
        }
    }

    int CalculateDamage()
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
