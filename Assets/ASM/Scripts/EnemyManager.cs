using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public float maxHP; 
    public int experiencePoints; 
    public float damage;
    public float speed; 
    public float attackRange; 
    public float currentHP; 
    private AnimationsController animationsController; 
    private enemyMove emove; 
    private bool isDie; 
    public float dropRate = 0.5f;
    private SkillUIManager skillUIManager; // Quản lý UI kỹ năng
    private SkillHolder playerSkillHolder; // Quản lý kỹ năng của người chơi

    public GameObject[] skillIconPrefabs; // Mảng prefab của icon kỹ năng
    public Transform playerTransform; // Transform của người chơi

    void Start()
    {
        isDie = false; 
        currentHP = maxHP; 
        animationsController = GetComponent<AnimationsController>(); 
        emove = GetComponent<enemyMove>(); 
        skillUIManager = FindObjectOfType<SkillUIManager>(); 
        playerSkillHolder = FindObjectOfType<SkillHolder>(); // Tìm và lưu tham chiếu đến SkillHolder
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Tìm Transform của người chơi
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount; 
        Debug.Log("Enemy took " + amount + " damage. Current health: " + currentHP);
        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            animationsController.Hit(); 
        }
    }

    void Die()
    {
        emove.isDead(); 
        FindAnyObjectByType<AudioManager>().Play("enemydead");
        gameObject.GetComponent<Collider>().enabled = false; 
        levelManager player = FindAnyObjectByType<levelManager>();
        if (player != null && !isDie)
        {
            player.EnemyDefeated(this); 
            isDie = true;
            DropSkill(); 
        }
        animationsController.SetDead(); 
        Destroy(gameObject, 2f); 
    }

    void DropSkill()
{
    if (playerSkillHolder != null && skillIconPrefabs != null && skillIconPrefabs.Length > 0)
    {
        if (Random.value <= dropRate)
        {
            int skillIndex = Random.Range(0, playerSkillHolder.skills.Count);

            // Kiểm tra nếu người chơi đã nhặt kỹ năng này rồi thì không rơi ra nữa
            if (!playerSkillHolder.skillReady[skillIndex])
            {
                playerSkillHolder.ActivateSkill(skillIndex);

                if (skillIndex < skillIconPrefabs.Length && skillIndex >= 0)
                {
                    GameObject skillIconPrefab = skillIconPrefabs[skillIndex];

                    // Tìm và instantiate dưới Canvas GAMEUI (nếu không tìm thấy thì không làm gì)
                    GameObject gameUI = GameObject.Find("GAMEUI");
                    if (gameUI != null)
                    {
                        Vector3 spawnPosition = transform.position;
                        GameObject skillIcon = Instantiate(skillIconPrefab, spawnPosition, Quaternion.Euler(90, 0, 0), gameUI.transform);

                        StartCoroutine(MoveSkillIconToPlayer(skillIcon));
                    }
                    else
                    {
                        Debug.LogWarning("GAMEUI not found. Skill icon not dropped.");
                    }
                }
            }
        }
    }
    else
    {
        Debug.LogWarning("playerSkillHolder or skillIconPrefabs not initialized.");
    }
}
    IEnumerator MoveSkillIconToPlayer(GameObject skillIcon)
{
    float duration = 1.2f; // Thời gian di chuyển
    Vector3 startPosition = skillIcon.transform.position;
    float elapsed = 0;

    while (elapsed < duration)
    {
        // Cập nhật vị trí hiện tại của người chơi
        Vector3 endPosition = playerTransform.position;

        skillIcon.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
        elapsed += Time.deltaTime;

        // Debug vị trí để kiểm tra xem icon đang di chuyển đúng
        Debug.Log("Skill icon position: " + skillIcon.transform.position);

        yield return null;
    }

    // Đảm bảo vị trí cuối cùng là vị trí của người chơi
    skillIcon.transform.position = playerTransform.position;
    Destroy(skillIcon); // Hủy icon kỹ năng sau khi tới người chơi
}

    public void DieByWave()
    {
        emove.isDead(); 
        FindAnyObjectByType<AudioManager>().Play("enemydead"); 
        gameObject.GetComponent<Collider>().enabled = false; 
        animationsController.SetDead(); 
        Destroy(gameObject, 2f); 
    }
}
