using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject enemies;
    public GameObject bosses;
    public int wave = 1;

    public LayerMask forbiddenLayer;
    public int maxAttempts = 100;

    private Transform player;
    public float spawnRadius = 10f;

    public float timeToNextSpawn = 3f;
    public GameObject bossEffect;

    private bool bossSpawned;
    public Slider waveBar;
    public Slider nextWaveBar;
    public TMP_Text waveText;
    public TMP_Text nextWaveText;

    public float countdownToWave = 60f; // Countdown timer for the current wave, adjustable from the Inspector
    private float transitionTimer = 7f; // Timer for the next wave transition
    private bool inTransition = false; // Flag to track if we are in transition

    public GameObject gameOverPanel; // Reference to the Game Over Panel

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
        waveBar.maxValue = countdownToWave; // Set max value of waveBar
        waveBar.value = countdownToWave; // Initialize waveBar to countdownToWave
        nextWaveBar.maxValue = transitionTimer; // Set max value of nextWaveBar
        nextWaveBar.value = transitionTimer; // Initialize nextWaveBar
        UpdateWaveText();
        nextWaveText.text = "";  // Initially hide next wave text

        waveBar.gameObject.SetActive(true); // Bắt đầu với waveBar hiển thị
        nextWaveBar.gameObject.SetActive(false); // Ẩn nextWaveBar ban đầu
        nextWaveText.gameObject.SetActive(false); // Ẩn nextWaveText ban đầu

        // Hide the Game Over Panel initially
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (enemyPrefabs == null)
        {
            return;
        }

        if (countdownToWave > 0 && !inTransition)
        {
            countdownToWave -= Time.deltaTime;
            waveBar.value = countdownToWave;
            UpdateWaveText();
            waveText.gameObject.SetActive(true); // Show waveText during countdown
            waveBar.gameObject.SetActive(true); // Show waveBar during countdown
            nextWaveBar.gameObject.SetActive(false); // Hide nextWaveBar during countdown
            nextWaveText.gameObject.SetActive(false); // Hide nextWaveText during countdown
            normalSpawn();
        }
        else if (transitionTimer > 0)
        {
            if (!inTransition)
            {
                inTransition = true;
                KillAllEnemies();
            }

            transitionTimer -= Time.deltaTime;
            nextWaveBar.value = transitionTimer;
            waveText.gameObject.SetActive(false); // Hide waveText during transition
            nextWaveText.text = "NEXT WAVE: " + (wave + 1);
            nextWaveText.gameObject.SetActive(true); // Show nextWaveText during transition
            waveBar.gameObject.SetActive(false); // Hide waveBar during transition
            nextWaveBar.gameObject.SetActive(true); // Show nextWaveBar during transition
        }
        else
        {
            // Transition to the next wave
            if (bosses.transform.childCount > 0)
            {
                // Nếu boss còn sống và hết thời gian, game sẽ kết thúc
                GameOver();
                return;
            }

            wave++;
            countdownToWave = 60f; // Reset countdownToWave to the set value
            transitionTimer = 7f; // Reset the transition timer
            waveBar.value = countdownToWave; // Reset waveBar
            nextWaveBar.value = transitionTimer; // Reset nextWaveBar
            nextWaveText.text = ""; // Hide next wave text
            nextWaveText.gameObject.SetActive(false); // Hide nextWaveText after transition
            waveText.gameObject.SetActive(true); // Show waveText after transition
            waveBar.gameObject.SetActive(true); // Show waveBar after transition
            nextWaveBar.gameObject.SetActive(false); // Hide nextWaveBar after transition
            bossSpawned = false;
            inTransition = false;
            waveSpawn();
        }

        if (bossSpawned && bosses.transform.childCount == 0)
        {
            countdownToWave = 60f;
            bossSpawned = false;
        }

        // Check for game over condition: if boss is still alive after wave time ends
        if (countdownToWave <= 0 && bosses.transform.childCount > 0)
        {
            GameOver();
        }
    }

    void normalSpawn()
    {
        if (!inTransition)
        {
            timeToNextSpawn -= Time.deltaTime;
            if (enemies.transform.childCount <= 100 && timeToNextSpawn <= 0)
            {
                SpawnRandomEnemy();
                timeToNextSpawn = Random.Range(2f, 5f);
            }
        }
    }

    void waveSpawn()
    {
        if (!inTransition)
        {
            timeToNextSpawn -= Time.deltaTime;
            if (enemies.transform.childCount <= 100 && timeToNextSpawn <= 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    SpawnRandomEnemy();
                }
                timeToNextSpawn = 5f;
            }

            if (!bossSpawned)
            {
                int randomMaxBoss = Random.Range(1, wave + 1);  // Include the current wave in range
                for (int i = 0; i < randomMaxBoss; i++)
                {
                    BossSpawn();
                }
                bossSpawned = true;
            }
        }
    }

    void KillAllEnemies()
    {
        foreach (Transform enemy in enemies.transform)
        {
            Destroy(enemy.gameObject);
        }
        foreach (Transform boss in bosses.transform)
        {
            Destroy(boss.gameObject);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += player.position;
        randomDirection.y = player.position.y;
        return randomDirection;
    }

    bool IsPositionValid(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 0.5f, forbiddenLayer);
        return colliders.Length == 0;
    }

    void SpawnRandomEnemy()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (IsPositionValid(randomPosition))
            {
                int randomPrefab = Random.Range(0, enemyPrefabs.Count);
                GameObject enemy = Instantiate(enemyPrefabs[randomPrefab], randomPosition, Quaternion.identity);
                enemy.transform.parent = enemies.transform;
                SetEnemyDetail(enemy);
                return;
            }
        }
    }

    void BossSpawn()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (IsPositionValid(randomPosition))
            {
                int randomPrefab = Random.Range(0, enemyPrefabs.Count);
                GameObject boss = Instantiate(enemyPrefabs[randomPrefab], randomPosition, Quaternion.identity);
                boss.transform.parent = bosses.transform;
                SetEnemyDetailBoss(boss);
                return;
            }
        }
    }

    private void SetEnemyDetail(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        emanager.maxHP += emanager.maxHP * wave * 0.1f;
        emanager.damage += emanager.damage * wave * 0.1f;
    }

    private void SetEnemyDetailBoss(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        enemy.transform.parent = bosses.transform;
        emanager.maxHP += (emanager.maxHP * wave * 0.1f) * 3;
        emanager.experiencePoints *= 2;
        emanager.attackRange *= 2;
        enemy.transform.localScale = new Vector3(1, 1, 1);
        emanager.damage += (emanager.damage * wave * 0.1f) * 2;
        Instantiate(bossEffect, enemy.transform);

    }

        void UpdateWaveText()
        {
            waveText.text = "WAVE: " + wave;
        }

        void GameOver()
        {
            Debug.Log("Game Over! Bạn đã không tiêu diệt được boss trong thời gian quy định.");
            gameOverPanel.SetActive(true); // Hiển thị panel Game Over
            Time.timeScale = 0f; // Dừng thời gian game
        }
    }



