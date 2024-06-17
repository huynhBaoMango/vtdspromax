using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float duration = 5f;
    public float damagePerSecond = 10f;
    private HashSet<GameObject> playersInZone = new HashSet<GameObject>();

    void Start()
    {
        StartCoroutine(DestroyAfterDuration());
    }

    IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInZone.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInZone.Remove(other.gameObject);
        }
    }

    void Update()
    {
        foreach (GameObject player in playersInZone)
        {
            // Gây sát thương cho người chơi
            player.GetComponent<PlayerHealth>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}
