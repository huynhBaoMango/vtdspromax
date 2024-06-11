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
    public float countdownToWave;
    public int wave = 1;

    public LayerMask forbiddenLayer;
    public int maxAttempts = 100;

    private Transform player;
    public float spawnRadius = 10f;

    public float timeToNextSpawn = 3f;

    public GameObject bossEffect;

    private bool bossSpawned;
    public Slider waveBar;
<<<<<<< HEAD
    public TMP_Text waveText;
=======
    public Slider nextWaveBar; // Thêm thanh nextWaveBar
    public TMP_Text waveText;
    public TMP_Text nextWaveText;

    public float countdownToWave = 60f; // Countdown timer for the current wave, adjustable from the Inspector
    private float transitionTimer = 7f; // Timer for the next wave transition
    private bool inTransition = false; // Flag to track if we are in transition
>>>>>>> parent of 295e3b6 (dada)

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
<<<<<<< HEAD
        countdownToWave = 60f;
        bossSpawned = false;
=======
        waveBar.maxValue = countdownToWave; // Set max value of waveBar
        waveBar.value = countdownToWave; // Initialize waveBar to countdownToWave
        nextWaveBar.maxValue = transitionTimer; // Set max value of nextWaveBar
        nextWaveBar.value = transitionTimer; // Initialize nextWaveBar
        UpdateWaveText();
        nextWaveText.text = "";  // Initially hide next wave text

        waveBar.gameObject.SetActive(true); // Bắt đầu với waveBar hiển thị
        nextWaveBar.gameObject.SetActive(false); // Ẩn nextWaveBar ban đầu
        nextWaveText.gameObject.SetActive(false); // Ẩn nextWaveText ban đầu
>>>>>>> parent of 295e3b6 (dada)
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPrefabs == null)
        {
            return;
        }
        countdownToWave -= 1 * Time.deltaTime;
        waveBar.value = countdownToWave;
        if (countdownToWave > 0)
        {
            
            normalSpawn();
        }
        else
        {
<<<<<<< HEAD
=======
            // Transition to the next wave
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
>>>>>>> parent of 295e3b6 (dada)
            waveSpawn();
        }

        if (bossSpawned)
        {
<<<<<<< HEAD
            if(bosses.transform.childCount == 0)
            {
                countdownToWave = 60f;
                wave++;
                bossSpawned = false; 
            }
=======
            countdownToWave = 60f;
            bossSpawned = false;
>>>>>>> parent of 295e3b6 (dada)
        }
    }


    void normalSpawn()
    {
        waveText.text = "NEXT WAVE: " + wave;
        timeToNextSpawn -= 1 * Time.deltaTime;
        if(enemies.transform.childCount <= 100 && timeToNextSpawn <= 0)
        {
            SpawnRandomEnemy();
            timeToNextSpawn = Random.Range(2f, 5f);
        }
    }

    void waveSpawn()
    {
        waveText.text = "WAVE " + wave;
        timeToNextSpawn -= 1 * Time.deltaTime;
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
            int randomMaxBoss = Random.Range(1, wave);
            for(int i = 1; i <= randomMaxBoss; i++)
            {
                BossSpawn();
            }
<<<<<<< HEAD
            bossSpawned = true; 
=======

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

    //het wave neu con enemy thi se giet het
    void KillAllEnemies()
    {
        foreach (Transform enemy in enemies.transform)
        {
            Destroy(enemy.gameObject);
        }
        foreach (Transform boss in bosses.transform)
        {
            Destroy(boss.gameObject);
>>>>>>> parent of 295e3b6 (dada)
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
                int randomPrefab = Random.Range(0, wave-1);
                if (wave-1 < enemyPrefabs.Count)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[randomPrefab], randomPosition, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;
                    setEnemyDetail(enemy);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomPosition, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;
                    setEnemyDetail(enemy);
                } 
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
                int randomPrefab = Random.Range(0, wave - 1);
                if (wave - 1 < enemyPrefabs.Count)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[randomPrefab], randomPosition, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;
                    setEnemyDetailBoss(enemy);
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomPosition, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;
                    setEnemyDetailBoss(enemy);
                }
                return;
            }
        }
    }

    private void setEnemyDetail(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        emanager.maxHP += (float)emanager.maxHP*wave*0.1f;
        emanager.damage += (float)emanager.damage*wave*0.1f;
    }
    private void setEnemyDetailBoss(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        enemy.transform.parent = bosses.transform;
        emanager.maxHP = (emanager.maxHP*3) + (emanager.maxHP * wave * 0.1f);
        emanager.experiencePoints *= 2;
        emanager.attackRange *= 2;
        enemy.transform.localScale = new Vector3(1, 1, 1);
        emanager.damage = (emanager.damage * 2) + (emanager.damage * wave * 0.1f);
        Instantiate(bossEffect, enemy.transform);
    }
<<<<<<< HEAD
=======

    void UpdateWaveText()
    {
        waveText.text = "WAVE: " + wave;
    }
>>>>>>> parent of 295e3b6 (dada)
}
