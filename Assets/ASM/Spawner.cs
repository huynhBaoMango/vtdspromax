using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    private GameObject player;
    public GameObject enemies;
    public float SpawnOffset;
    public float countdownToWave;
    public float timeToSpawn = 0;
    public int wave = 1;
    void Start()
    {
        player = GameObject.Find("PLAYER");
        countdownToWave = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPrefabs == null)
        {
            return;
        }
        if (countdownToWave > 0)
        {
            countdownToWave -= 1 * Time.deltaTime;
            timeToSpawn -= 1 * Time.deltaTime;
            if (timeToSpawn <= 0)
            {
                for (int i = 0; i <= wave; i++)
                {
                    Vector3 randomSpawnNearPlayer = new Vector3(Random.Range(player.transform.position.x - SpawnOffset, player.transform.position.x + SpawnOffset), 1, Random.Range(player.transform.position.z - SpawnOffset, player.transform.position.z + SpawnOffset));
                    var enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomSpawnNearPlayer, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;

                }
                timeToSpawn = 3f;
            }
        }
        else
        {
            Debug.Log("IN WAVE");
            timeToSpawn -= 1 * Time.deltaTime;
            if(enemies.transform.childCount <= wave * 10 && enemies.transform.childCount <= 500)
            {
                for (int i = 0; i <= wave; i++)
                {
                    Vector3 randomSpawnNearPlayer = new Vector3(Random.Range(player.transform.position.x - SpawnOffset, player.transform.position.x + SpawnOffset), 1, Random.Range(player.transform.position.z - SpawnOffset, player.transform.position.z + SpawnOffset));
                    var enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], randomSpawnNearPlayer, Quaternion.identity);
                    enemy.transform.parent = enemies.transform;


                }
            }
            timeToSpawn = 3f;
        }
        
    }
}
