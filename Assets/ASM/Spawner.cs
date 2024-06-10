using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public GameObject enemies;
    public float countdownToWave;
    public int wave = 1;

    public LayerMask forbiddenLayer;
    public int maxAttempts = 100;

    private Transform player;
    public float spawnRadius = 10f;

    public float timeToNextSpawn = 3f;

    void Start()
    {
        player = GameObject.Find("PLAYER").transform;
        countdownToWave = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPrefabs == null)
        {
            return;
        }
        countdownToWave -= 1 * Time.deltaTime;
        if (countdownToWave > 0)
        {
            normalSpawn();
        }
        else
        {
            waveSpawn();
        }
    }


    void normalSpawn()
    {
        timeToNextSpawn -= 1 * Time.deltaTime;
        if(enemies.transform.childCount <= 100 && timeToNextSpawn <= 0)
        {
            SpawnRandomEnemy();
            timeToNextSpawn = Random.Range(2f, 5f);
        }
    }

    void waveSpawn()
    {

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
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomPosition, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;
                }
                break;
            }
        }
    }

    private void setEnemyDetail(GameObject enemy)
    {
        EnemyManager emanager = enemy.GetComponent<EnemyManager>();
        emanager.maxHP += (float)emanager.maxHP*wave*0.1f;
        emanager.damage += (float)emanager.damage*wave*0.1f;
    }
}
