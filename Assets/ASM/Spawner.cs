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
        countdownToWave = 60f;
        timeToSpawn = 3f;
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

    }

    void waveSpawn()
    {

    }
}
