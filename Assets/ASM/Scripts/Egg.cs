using System.Collections;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public GameObject damageZonePrefab;
    public float fallImpactForce = 5f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > fallImpactForce)
        {
            Instantiate(damageZonePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
