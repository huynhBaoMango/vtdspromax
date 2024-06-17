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
    private SkillUIManager skillUIManager; 
    private SkillHolder playerSkillHolder; 

    public GameObject[] skillIconPrefabs; 
    public Transform playerTransform; 

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

           
            if (!playerSkillHolder.skillReady[skillIndex])
            {
                playerSkillHolder.ActivateSkill(skillIndex);

                if (skillIndex < skillIconPrefabs.Length && skillIndex >= 0)
                {
                    GameObject skillIconPrefab = skillIconPrefabs[skillIndex];

                   
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
    float duration = 1.2f; 
    Vector3 startPosition = skillIcon.transform.position;
    float elapsed = 0;

    while (elapsed < duration)
    {
        
        Vector3 endPosition = playerTransform.position;

        skillIcon.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
        elapsed += Time.deltaTime;

        
        Debug.Log("Skill icon position: " + skillIcon.transform.position);

        yield return null;
    }

    
    skillIcon.transform.position = playerTransform.position;
    Destroy(skillIcon);
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
