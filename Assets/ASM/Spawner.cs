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
    public TMP_Text waveText;

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
        countdownToWave = 60f;
        bossSpawned = false;
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
            waveSpawn();
        }

        if (bossSpawned)
        {
            if (bosses.transform.childCount == 0)
            {
                wave++;
                countdownToWave = 60f;
                bossSpawned = false;
            }
        }
    }


    void normalSpawn()
    {
        waveText.text = "NEXT WAVE: " + wave;
        timeToNextSpawn -= 1 * Time.deltaTime;
        if (enemies.transform.childCount <= 100 && timeToNextSpawn <= 0)
        {
            SpawnRandomEnemy();
            timeToNextSpawn = Random.Range(2f, 5f);
        }
    }

    void waveSpawn()
    {
        waveText.text = "WAVE" + wave;
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
            for (int i = 1; i <= randomMaxBoss; i++)
            {
                BossSpawn();
            }
            bossSpawned = true;
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
                int randomPrefab = Random.Range(0, wave - 1);
                if (wave - 1 < enemyPrefabs.Count)
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
        emanager.maxHP += (float)emanager.maxHP * wave * 0.1f;
        emanager.damage += (float)emanager.damage * wave * 0.1f;
    }
    private void setEnemyDetailBoss(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        enemy.transform.parent = bosses.transform;
        emanager.maxHP = (emanager.maxHP * 3) + (emanager.maxHP * wave * 0.1f);
        emanager.experiencePoints *= 2;
        emanager.attackRange *= 2;
        enemy.transform.localScale = new Vector3(1, 1, 1);
        emanager.damage = (emanager.maxHP * 2) + (emanager.damage * wave * 0.1f);
        Instantiate(bossEffect, enemy.transform);
    }
}
